using System;
using Xunit;
using SlackerRunner;


namespace SlackerRunner.IntegrationTests
{
  public class ImpersonatorTester
  {

    [Fact]
    public void SmokeImpersonatorInvalidCreds()
    {
      Exception ex = Record.Exception(() =>
      {
        using (Impersonator pers = new Impersonator("invalid_user", "invalid_domain", "invalid_password"))
        {
          // Invalid user, this doesn't hit 
        }
      });

      // Exception was thrown
      Assert.NotNull(ex);
    }

  }  // EOF
}

