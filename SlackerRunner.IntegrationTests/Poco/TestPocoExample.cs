using System;
//
using Xunit;
//
using SlackerRunner.Poco;


namespace SlackerRunner.IntegrationTests.Poco
{
  public class TestPocoExample
  {
    
    /// <summary>
    /// Coverage test, these haven't been touched by other test code
    /// </summary>
    [Fact]
    public void smokeTest()
    {
      Example sample = new Example();
      
      // excersize some props
      sample.description = "description";
      sample.pending_message = "pending";
      sample.line_number = 2;

      // Proof it 
      Assert.NotEmpty(sample.description);
      Assert.NotEmpty(sample.pending_message);
      Assert.True(sample.line_number>0);
    }

  }
}

