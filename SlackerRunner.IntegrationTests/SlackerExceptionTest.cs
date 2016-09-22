using System;
using System.IO;
using Xunit;
using System.ComponentModel;
using SlackerRunner.Util;


namespace SlackerRunner.IntegrationTests
{
  class SlackerExceptionTest
  {

    [Fact]
    public async void SmokeTest_SlackerException()
    {
      // Smoke testing SlackerException
      Exception ex = await Record.ExceptionAsync(() =>
      {
        throw new SlackerException();
      });

      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
    }

    [Fact]
    public async void SmokeTest_SlackerExceptionWithMessage()
    {
      // Smoke testing SlackerException
      string msg = "My message";
      Exception ex = await Record.ExceptionAsync(() =>
      {
        throw new SlackerException(msg);
      });

      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True(ex.Message.Equals(msg));
    }
  }
}
