using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SlackerRunner
{
  public class Logger
  {
    /// <summary>
    /// Logs out the passed text to output
    /// </summary>
    public static void Log(string Text)
    {
      Log( null, Text);
    }

    /// <summary>
    /// Logs out the passed text to output
    /// </summary>
    public static void Log( TestContext testContextInstance, string Text)
    {
      string textlineOut = "~~~ " + Text;

      // Log it
      Trace.WriteLine(textlineOut);
      
      // And MS output if avail
      if (testContextInstance != null)
        testContextInstance.WriteLine(textlineOut);

    }
    

  }
}
