using System.Diagnostics;
using System.IO;

namespace SlackerRunner
{
    public class ProcessStartInfoBuilder : IProcessStartInfoBuilder
    {
        public ProcessStartInfo Build(string testDirectory, string batchFileName, string profile, string outputFileName)
        {
            //ProcessStartInfo proc = new ProcessStartInfo();

            return new ProcessStartInfo
                       {
                           FileName = "cmd.exe",
                           // Directory name needs to be enclosed in double quotes 
                           Arguments = "/c \"" + Path.Combine(testDirectory, batchFileName) + "\" " + profile +" " + outputFileName,
                           WorkingDirectory = Path.GetDirectoryName(testDirectory), 
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