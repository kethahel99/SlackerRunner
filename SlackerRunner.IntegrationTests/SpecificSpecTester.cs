using System;
using Xunit;
using SlackerRunner;


namespace SlackerRunner.IntegrationTests
{
  public class SpecificSpecTester
  {

    /// <summary>
    /// Runs the Spec test in the file specified 
    /// </summary>
    /// <param name="rbFile"></param>
    [Fact]
    public void RunThisSpec()
    {
      Assert.True(TestFile(@"sample\sample1.rb"), "Test should have succeeded.");
    }


    // runs the passed file and reports on the result
    private bool TestFile(string specFile)
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + specFile );
      return SlackerResults.Passed;
    }

  }
}
