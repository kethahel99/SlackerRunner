using System;
using Xunit;
using SlackerRunner;


namespace SlackerRunner.IntegrationTests
{
  public class SpecificSpecTester
  {
    /// <summary>
    /// Runs the slacker test in the file specified 
    /// </summary>
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunThisSpec()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb");
      Assert.True(SlackerResults.Passed);
    }

  }
}
