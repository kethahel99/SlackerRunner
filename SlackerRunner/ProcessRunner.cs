using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;


namespace SlackerRunner
{
  public class ProcessRunner : IDisposable
  {
    private StringBuilder _StandardOutput = new StringBuilder();
    private StringBuilder _ErrorOutput = new StringBuilder();
    AutoResetEvent _outputWaitHandle = new AutoResetEvent(false);
    AutoResetEvent _errorWaitHandle = new AutoResetEvent(false);
    private Process _process = new Process();

    public ProcessRunner()
    {
      Logger.Log("Slacker runner, version=" + RuntimeVersion.GetVersion());
      // Set default timeout 
      TimeoutMilliseconds = ProfileRunner.DEFAULT_TIMEOUT;
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
    public void RunDirectory(string testDirectory, string specDirectory, int timeoutMilliseconds)
    {
      TimeoutMilliseconds = timeoutMilliseconds;
      this.Run(testDirectory, specDirectory, string.Empty);
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
      using (_process = new Process())
      {
        // set the attributes
        ProcessStartInfo procSI = new ProcessStartInfo();
        procSI.FileName = "cmd.exe";
        
        // Run directory or file 
        if (specDirectory != null && specDirectory != string.Empty)
        {
          // Make sure the path ends with \ otherwise add it
          if (!specDirectory.EndsWith(@"\"))
            specDirectory = specDirectory + @"\";

          // Run everything in the directory and subdirectories 
          // as specDirectory has been specified 
          // **\* means all specs in dir and sub directories 
          // -fj means Json format, -fh ( HTML ), -fd ( document )
          procSI.Arguments = "/C slacker \"" + specDirectory + "**\\*\" -fj";
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
        _process.StartInfo = procSI;

        // Retrieve the outputs
        RegisterToEvents();

          // Go for it 
        _process.Start();
          _process.BeginOutputReadLine();
          _process.BeginErrorReadLine();


          // Make sure there is no timeout issues
          if (_process.WaitForExit(TimeoutMilliseconds) &&
            _outputWaitHandle.WaitOne(TimeoutMilliseconds) &&
            _errorWaitHandle.WaitOne(TimeoutMilliseconds))
          {
            // Ensure event handling completion
            // https://stackoverflow.com/questions/24543306/process-errordatareceived-fired-after-process-is-disposed
            _process.WaitForExit();

            // Process completed
            Logger.Log("process ended, exitcode=" + _process.ExitCode);
            Logger.Log("process standard out=" + _StandardOutput);
            // Throws when Slacker has an error
            if (_process.ExitCode != 0)
              throw new SlackerException("Slacker error, exitcode=" + _process.ExitCode, new Exception(StandardOutput + " - " + StandardError));
          }
          else
          {
            // Timed out.
            Logger.Log("run timeout");
            // Throws when Slacker times out
            throw new SlackerException("SlackerRunner timeout error, timeout is set to=" + (TimeoutMilliseconds / 1000) + " seconds");
          }

          // Advertise
          if (StandardError != "")
            Logger.Log("Error=" + StandardError);
        
      }
    }

    private void RegisterToEvents()
    {
      _process.OutputDataReceived += process_OutputDataReceived;
      _process.ErrorDataReceived += process_ErrorDataReceived;
    }

    private void UnregisterFromEvents()
    {
      _process.OutputDataReceived -= process_OutputDataReceived;
      _process.ErrorDataReceived -= process_ErrorDataReceived;
    }

    private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      if (e.Data == null)
      {
        _errorWaitHandle.Set();
      }
      else
      {
        _ErrorOutput.AppendLine(e.Data);
      }
    }

    private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      if (e.Data == null)
      {
        _outputWaitHandle.Set();
      }
      else
      {
        _StandardOutput.AppendLine(e.Data);
      }
    }

    /// <summary>
    /// Cleanup process and handles as needed 
    /// </summary>
    public void Dispose()
    {
      UnregisterFromEvents();

      if (_process != null)
      {
        _process.Dispose();
        _process = null;
      }

      if (_errorWaitHandle != null)
      {
        _errorWaitHandle.Close();
        _outputWaitHandle = null;
      }

      if (_outputWaitHandle != null)
      {
        _outputWaitHandle.Close();
        _outputWaitHandle = null;
      }

    }

  }  // EOF
}