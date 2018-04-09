using System;
using Xunit;


namespace SlackerRunner.IntegrationTests
{
  public class SlackerExceptionTest
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
      Assert.Equal(ex.Message, msg);
    }

    [Fact]
    public async void SmokeTest_SlackerExceptionWithMessageAndInner()
    {
      // Smoke testing SlackerException
      string msg = "My message";
      string innerMsg = "Null ref detected, inner exception";
      Exception ex = await Record.ExceptionAsync(() =>
      {
        throw new SlackerException(msg, new NullReferenceException(innerMsg));
      });

      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.Equal(ex.Message, msg);
      // Inner
      Assert.True(ex.InnerException is NullReferenceException);
      Assert.Equal(ex.InnerException.Message, innerMsg);
    }

  }
}
