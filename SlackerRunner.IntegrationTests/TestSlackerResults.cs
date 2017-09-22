using System;
//
using Xunit;



namespace SlackerRunner.IntegrationTests
{
  public class TestSlackerResults
  {
    
    /// <summary>
    /// Coverage test, these haven't been touched by other test code
    /// </summary>
    [Fact]
    public void SmokeTest()
    {
      SlackerResults res = new SlackerResults();

      // excersize some props
      res.File = "file";

      // Proof it 
      Assert.NotEmpty(res.File);
      Assert.NotEmpty(res.ToString());
    }

  }
}

