using System;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;

namespace SlackerRunner.Xunit
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  [XunitTestCaseDiscoverer("SlackerRunner.Xunit.XunitExtensions.SlackerFactDiscoverer", "SlackerRunner")]
  public sealed class SlackerRunnerTheoryAttribute : TheoryAttribute
  {
    public SlackerRunnerTheoryAttribute([CallerMemberName] string memberName = null)
    {
      DisplayName = memberName;
    }
  }

}
