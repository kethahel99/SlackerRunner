using System;
using System.IO;
using System.Diagnostics;

namespace SlackerRunner
{
  public class Logger
  {

    /// <summary>
    /// Logs out the passed text to output
    /// </summary>
    public static void Log(string Text)
    {
      //DateTime time = DateTime.Now;
      //string theTime = String.Format("{0:d/M/yyyy HH:mm:ss:fff}", time);
      //string textlineOut = "~~~ " + theTime + ", " + Text + Environment.NewLine;
      string textlineOut = "~~~ " + Text;

      // Log it
      Console.WriteLine(textlineOut);
      Trace.WriteLine(textlineOut);
    }

  }
}
