using System;
using Xunit;
using SlackerRunner;

namespace SlackerRunner.IntegrationTests
{
  public class ProcessRunnerTest
  {
    
    [Fact]
    public void SmokeTest()
    {
      ProcessRunner prun = new ProcessRunner();
      // validate
      Assert.NotNull(prun);
      Assert.NotNull(prun.StandardError);
      Assert.NotNull(prun.StandardOutput);
      Assert.NotNull(prun.ToString());
      // should fail
      Exception ex = Record.Exception(() =>
      {
        prun.Run("fake", "fake");
      });
      // Make sure to get SlackerException on fake input
      Assert.Equal(typeof(SlackerException), ex.GetType());
    }

  }  // EOC
}