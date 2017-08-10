using System;
using System.Diagnostics;
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

    /// <summary>
    /// Runs the slacker tests that match the criteria *.rb
    /// </summary>
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunSpecs()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\*.rb");
      Assert.True( SlackerResults.PassedSpecs > 1, SlackerResults.Message );
    }

    /// <summary>
    /// Runs the slacker tests that match the criteria 
    /// </summary>
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunAllSpecsIncludingSubdirs()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\**\*");
      Assert.True(SlackerResults.PassedSpecs > 3, SlackerResults.Message);
    }


  }
}
