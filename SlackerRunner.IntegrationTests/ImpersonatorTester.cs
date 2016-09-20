using System;
using Xunit;
using SlackerRunner;


namespace SlackerRunner.IntegrationTests
{
  public class ImpersonatorTest
  {

    [Fact]
    public void SmokeImpersonator()
    {
      Exception ex = Record.Exception(() =>
      {
        using (Impersonator pers = new Impersonator("invalid_user", "invalid_domain", "invalid_password"))
        {
          // Invalid user, null  
          Assert.Null(pers);
        }
      });

    }


  }  // EOF
}

