using System.Diagnostics;
using System.IO;

namespace SlackerRunner
{
    public class ProcessStartInfoBuilder : IProcessStartInfoBuilder
    {
        public ProcessStartInfo Build(string testDirectory, string batchFileName, string profile, string outputFileName)
        {
            return new ProcessStartInfo
                       {
                           FileName = "cmd.exe",
                           Arguments = "/c " + Path.Combine(testDirectory, batchFileName) + " " + profile +" " + outputFileName,
                           WorkingDirectory = testDirectory,
                           UseShellExecute = false,
                           RedirectStandardInput = true,
                           RedirectStandardOutput = true,
                           RedirectStandardError = true,
                           WindowStyle = ProcessWindowStyle.Hidden,
                           CreateNoWindow = true
                       };
        }
    }
}