using System.Diagnostics;
using System.IO;

namespace SlackerRunner
{
    public class ProcessStartInfoBuilder : IProcessStartInfoBuilder
    {
        public ProcessStartInfo Build(string testDirectory, string batchFileName, string profile, string outputFileName)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            
            // set the attributes
            proc.FileName = "cmd.exe";
            // Directory name needs to be enclosed in double quotes 
            proc.Arguments = "/C " + batchFileName + " \"" + profile + "\" " + outputFileName;
            proc.WorkingDirectory = "" + Path.GetDirectoryName(testDirectory) + "";
            proc.UseShellExecute = false;
            proc.RedirectStandardInput = false;
            proc.RedirectStandardOutput = true;
            proc.RedirectStandardError = true;
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.CreateNoWindow = true;

            return proc;
        }
    }
}