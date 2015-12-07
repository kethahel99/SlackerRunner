using System.Diagnostics;
using System.IO;
using Xunit;

namespace SlackerRunner.UnitTests
{
    public class ProcessStartInfoBuilderTest
    {
        [Fact]
        public void BuildReturnsStartInfoWithCorrectParameters()
        {
            const string testDirectory = "F://AutomatedTests/";
            const string outputFileName = "output.txt";
            const string batchFileName = "run.bat";
            const string profile = "default";

            ProcessStartInfo processStartInfo = new ProcessStartInfoBuilder().Build(testDirectory, batchFileName, profile, outputFileName);

            Assert.Equal(processStartInfo.FileName, "cmd.exe");
            Assert.Equal(processStartInfo.Arguments, "/C " + batchFileName + " \"" + profile + "\" " + outputFileName);
            Assert.Equal(processStartInfo.WorkingDirectory, Path.GetDirectoryName(testDirectory) );
            Assert.False(processStartInfo.UseShellExecute);
            Assert.False(processStartInfo.RedirectStandardInput);
            Assert.True(processStartInfo.RedirectStandardOutput);
            Assert.True(processStartInfo.RedirectStandardError);
            Assert.Equal(processStartInfo.WindowStyle, ProcessWindowStyle.Hidden);
            Assert.True(processStartInfo.CreateNoWindow);
        }
    }
}