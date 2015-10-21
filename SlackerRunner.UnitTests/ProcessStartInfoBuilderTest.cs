using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackerRunner.UnitTests
{
    [TestClass]
    public class ProcessStartInfoBuilderTest
    {
        [TestMethod]
        public void BuildReturnsStartInfoWithCorrectParameters()
        {
            const string testDirectory = "F://AutomatedTests/";
            const string outputFileName = "output.txt";
            const string batchFileName = "run.bat";
            const string profile = "default";

            ProcessStartInfo processStartInfo = new ProcessStartInfoBuilder().Build(testDirectory, batchFileName, profile, outputFileName);

            Assert.AreEqual(processStartInfo.FileName, "cmd.exe");
            Assert.AreEqual(processStartInfo.Arguments, "/c \"" + Path.Combine(testDirectory, batchFileName) + "\" " + profile + " " + outputFileName);
            Assert.AreEqual(processStartInfo.WorkingDirectory, Path.GetDirectoryName(testDirectory) );
            Assert.IsFalse(processStartInfo.UseShellExecute);
            Assert.IsTrue(processStartInfo.RedirectStandardInput);
            Assert.IsTrue(processStartInfo.RedirectStandardOutput);
            Assert.IsTrue(processStartInfo.RedirectStandardError);
            Assert.AreEqual(processStartInfo.WindowStyle, ProcessWindowStyle.Hidden);
            Assert.IsTrue(processStartInfo.CreateNoWindow);
        }
    }
}