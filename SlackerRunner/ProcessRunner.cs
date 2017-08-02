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
    private StringBuilder _StandardOutput = new StringBuilder();
    private StringBuilder _ErrorOutput = new StringBuilder();

    public ProcessRunner()
    {
      Logger.Log("Slacker runner, version=" + RuntimeVersion.GetVersion());
    }

    public string StandardOutput
    {
      get { return _StandardOutput.ToString(); }
    }
    public int TimeoutMilliseconds
    {
      set;
      get;
    }
    public string StandardError
    {
      get { return _ErrorOutput.ToString(); }
    }

    /// <summary>
    /// Runs all specs in a directory
    /// </summary>
    public void RunDirectory(string testDirectory, string specDirectory, int timeoutMilliseconds )
    {
      TimeoutMilliseconds = timeoutMilliseconds;
      this.Run(testDirectory, specDirectory, string.Empty );
    }

    /// <summary>
    /// Runs a spec file in directory
    /// </summary>
    public void Run(string testDirectory, string specFile)
    {
      this.Run(testDirectory, string.Empty, specFile );
    }

    /// <summary>
    /// Runs the Slacker process on directory or file
    /// </summary>
    private void Run(string testDirectory, string specDirectory, string specFile)
    {
      // Shooting off a process 
      using (var process = new Process())
      {
        // set the attributes
        ProcessStartInfo procSI = new ProcessStartInfo();
        procSI.FileName = "cmd.exe";
        // Run either in dir or single file 
        if (specDirectory  != null && specDirectory != string.Empty)
        {
          // Run everything in the directory as no profile / file was specified 
          procSI.Arguments = "/C slacker";
        }
        else
        {
          // File name needs to be enclosed in double quotes 
          procSI.Arguments = "/C slacker" + " \"" + specFile + "\" ";
        }

        // set the process attributes
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
          // General output
          process.OutputDataReceived += (sender, e) =>
          {
            if (e.Data == null)
            {
              outputWaitHandle.Set();
            }
            else
            {
              _StandardOutput.AppendLine(e.Data);
            }
          };

          // Error 
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
          if (process.WaitForExit(TimeoutMilliseconds) &&
            outputWaitHandle.WaitOne(TimeoutMilliseconds) &&
            errorWaitHandle.WaitOne(TimeoutMilliseconds))
          {
            // Process completed
            Logger.Log("process ended, exitcode=" + process.ExitCode);
            Logger.Log("process standard out=" + _StandardOutput);
            // Throws when Slacker has an error
            if (process.ExitCode != 0)
              throw new SlackerException("Slacker error, exitcode=" + process.ExitCode, new Exception(StandardOutput + " - " + StandardError));
          }
          else
          {
            // Timed out.
            Logger.Log("run timeout");
            // Throws when Slacker times out
            throw new SlackerException("Slacker timeout error, timeout is set to=" + (TimeoutMilliseconds / 1000) + " seconds");
          }

          // Advertise
          if (StandardError != "")
            Logger.Log("Error=" + StandardError);
        }
      }
    }



  }  // EOF
}