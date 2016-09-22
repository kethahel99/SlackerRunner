using System;
using Xunit;
using SlackerRunner;

namespace SlackerRunner.IntegrationTests
{
  public class ProfileRunnerTest
  {
    
    [Fact]
    public void InvalidCondition()
    {
      ProfileRunner run = null;
      Exception ex = Record.Exception(() =>
      {
        run = new ProfileRunner();
        run.Run("invalid_dir", "invalid_file");
      });
      
      // Should throw SlackerException
      Assert.Equal(typeof(SlackerException), ex.GetType());
      Assert.NotNull(run);
    }
    
  }  // EOC
}