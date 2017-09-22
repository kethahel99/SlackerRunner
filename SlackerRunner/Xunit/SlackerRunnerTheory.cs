using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace SlackerRunner.Xunit
{
  public sealed class SlackerRunnerTheoryAttribute : TheoryAttribute
  {
    public SlackerRunnerTheoryAttribute([CallerMemberName] string memberName = null)
    {
      DisplayName = memberName;
    }
  }

}
