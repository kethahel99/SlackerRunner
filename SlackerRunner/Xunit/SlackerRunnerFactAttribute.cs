using System;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;

namespace SlackerRunner.Xunit
{

  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  [XunitTestCaseDiscoverer("SlackerRunner.Xunit.XunitExtensions.SlackerFactDiscoverer", "SlackerRunner")]
  public sealed class SlackerRunnerFactAttribute : FactAttribute
  {
    
    public SlackerRunnerFactAttribute([CallerMemberName] string memberName = null)
    {
      DisplayName = memberName;
    }
    
  }

}
