using System;
//
using Xunit;
using SlackerRunner.Poco;


namespace SlackerRunner.IntegrationTests.Poco
{
  public class TestPocoException
  {
    
    /// <summary>
    /// Coverage test, these haven't been touched by other test code
    /// </summary>
    [Fact]
    public void SmokeTest()
    {
      SlackerRunner.Poco.Exception tmp = new SlackerRunner.Poco.Exception();
      
      // excersize some props
      tmp.Class = "class";

      // Proof it 
      Assert.NotEmpty(tmp.Class);
    }

  }
}

