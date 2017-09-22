using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace SlackerRunner.Xunit
{
  public sealed class SlackerRunnerFactAttribute : FactAttribute
  {
    public SlackerRunnerFactAttribute([CallerMemberName] string memberName = null)
    {
      DisplayName = memberName;
    }
  }

}
