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

    public ProcessRunner()
    {
      Console.WriteLine("Slacker runner, version=" + RuntimeVersion.GetVersion());
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

          process.Start();
          process.BeginOutputReadLine();
          process.BeginErrorReadLine();

          if (process.WaitForExit(15000) &&
              outputWaitHandle.WaitOne(15000) &&
              errorWaitHandle.WaitOne(15000))
          {
            // Process completed. Check process.ExitCode here.
            Console.WriteLine("~~~ process ended, exitcode=" + process.ExitCode);
            Console.WriteLine("~~~ process standard out=" + _StandArdOutput );
          }
          else
          {
            // Timed out.
            Console.WriteLine("~~~ timeout");
          }

          // Advertise
          if (StandardError != "")
            Console.WriteLine("Error=" + StandardError);
        }
      }
    }
/*
    void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
      //* Do your stuff with the output (write to console/log/StringBuilder)
      _StandArdOutput.Append(outLine.Data);
      Console.WriteLine("~~~" + outLine.Data);
    }

    void ErrorOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
      //* Do your stuff with the output (write to console/log/StringBuilder)
      _ErrorOutput.Append( outLine.Data );
      Console.WriteLine("~~~" + outLine.Data);
    }
    */

  }
}