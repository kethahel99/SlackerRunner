using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace SlackerRunner.UnitTests
{
    [TestClass]
    public class ProcessRunnerTest
    {
        [TestMethod]
        public void RunCallsFactoyToCreateProcessWrapperAndRunsProcess()
        {
            var process = MockRepository.GenerateStub<IProcess>();
            Func<IProcess> processCreator = () => process;
            var processStartInfoBuilder = MockRepository.GenerateMock<IProcessStartInfoBuilder>();
            processStartInfoBuilder.Expect(x => x.Build("testDirectory", "run.bat", "default", "output.txt")).Return(new ProcessStartInfo());
            process.Expect(x => x.Start()).Return(true);
            process.Expect(x => x.WaitForExit());
            process.Expect(x => x.StandardError).Return(new StreamReader(new MemoryStream(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes("StandardError"))));
            process.Expect(x => x.StandardOutput).Return(new StreamReader(new MemoryStream(System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes("StandardOutput"))));
            process.Expect(x => x.Dispose());

            var processRunner = new ProcessRunner(processStartInfoBuilder, processCreator);
            processRunner.Run("testDirectory", "run.bat", "default", "output.txt");

            Assert.AreEqual("StandardError", processRunner.StandardError);
            Assert.AreEqual("StandardOutput", processRunner.StandardOutput);
            processStartInfoBuilder.VerifyAllExpectations();
            process.VerifyAllExpectations();
        }
    }
}