using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace SlackerRunner
{
  public class ProcessRunner
  {
    private StringBuilder _StandArdOutput = new StringBuilder();
    private StringBuilder _ErrorOutput = new StringBuilder();
    // Default timeout for individual test
    private int SlackerTimeOutValueMillisec = 60 * 1000;


    public ProcessRunner()
    {
      Logger.Log("Slacker runner, version=" + RuntimeVersion.GetVersion());
    }

    public string StandardOutput
    {
      get { return _StandArdOutput.ToString(); }
    }
    public string StandardError
    {
      get { return _ErrorOutput.ToString(); }
    }

    public void Run( string testDirectory, string profile )
    {
      using (var process = new Process())
      {

        // set the attributes
        ProcessStartInfo procSI = new ProcessStartInfo();
        procSI.FileName = "cmd.exe";
        // File name needs to be enclosed in double quotes 
        procSI.Arguments = "/C slacker" + " \"" + profile + "\" ";
        procSI.WorkingDirectory = Path.GetDirectoryName(testDirectory);
        procSI.UseShellExecute = false;
        procSI.RedirectStandardInput = false;
        procSI.RedirectStandardOutput = true;
        procSI.RedirectStandardError = true;
        procSI.WindowStyle = ProcessWindowStyle.Hidden;
        procSI.CreateNoWindow = true;
        // process start info
        process.StartInfo = procSI;

        // Retrieve the outputs
        using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
        using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
        {
          process.OutputDataReceived += (sender, e) =>
          {
            if (e.Data == null)
            {
              outputWaitHandle.Set();
            }
            else
            {
              _StandArdOutput.AppendLine(e.Data);
            }
          };

          process.ErrorDataReceived += (sender, e) =>
          {
            if (e.Data == null)
            {
              errorWaitHandle.Set();
            }
            else
            {
              _ErrorOutput.AppendLine(e.Data);
            }
          };

          // Go for it 
          process.Start();
          process.BeginOutputReadLine();
          process.BeginErrorReadLine();


          // Make sure there is no timeout issues
          if (process.WaitForExit(SlackerTimeOutValueMillisec) && 
            outputWaitHandle.WaitOne(SlackerTimeOutValueMillisec) && 
            errorWaitHandle.WaitOne(SlackerTimeOutValueMillisec))
          {
            // Process completed
            Logger.Log("process ended, exitcode=" + process.ExitCode);
            Logger.Log("process standard out=" + _StandArdOutput );
            // Throws when Slacker has an error
            if (process.ExitCode != 0)
              throw new SlackerException("Slacker error, exitcode=" + process.ExitCode);
          }
          else
          {
            // Timed out.
            Logger.Log("run timeout");
            // Throws when Slacker times out
            throw new SlackerException("Slacker timeout error, default timeout is set to=" + ( SlackerTimeOutValueMillisec  / 1000 ) + " seconds" );
          }

          // Advertise
          if (StandardError != "")
            Logger.Log("Error=" + StandardError);
        }
      }
    }



  }  // EOF
}