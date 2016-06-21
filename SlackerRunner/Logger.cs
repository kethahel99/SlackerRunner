using System;
using System.IO;
using System.Diagnostics;

namespace SlackerRunner
{
  public class Logger
  {
    private static bool _runAlready = false;

    /// <summary>
    /// Logs out the passed text to output
    /// </summary>
    public static void Log(string Text)
    {
      //  Trace to the output when available
      if (!_runAlready)
      {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        _runAlready = true;
      }

      DateTime time = DateTime.Now;
      string theTime = String.Format("{0:d/M/yyyy HH:mm:ss:fff}", time);
      string textlineOut = "~~~ " + theTime + ", " + Text + Environment.NewLine;

      // Log it
      //Console.WriteLine(textlineOut);
      Trace.WriteLine(textlineOut);
    }

  }
}
