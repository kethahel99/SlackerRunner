using System;
using System.Reflection;

namespace SlackerRunner
{
  public class RuntimeVersion
  {
    public static string GetVersion()
    {
      // Get version by reflection from AssemblyInfo
      return typeof(RuntimeVersion).Assembly.GetName().Version.ToString();
    }
  }
}
