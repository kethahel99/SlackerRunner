using System;
using System.IO;
using System.Collections.Generic;
//
using Xunit;
//
using SlackerRunner.Util;


namespace SlackerRunner.IntegrationTests
{
  public class SpecsDirectoryTester
  {
    // Relative path to test dir 
    public static string RUN_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests")) + "\\";
    public static string LONG_SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec", "sam ple") + "/");


    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void runAllSpecsInDirectory()
    {
      // Use explicit timeout as there are a few tests run at the same time ( ** )
      int timeoutMilliseconds = 300 * 1000;
      // Run all the tests in the directory at once 
      SlackerResults SlackerResults = new SlackerService().RunDirectory(RUN_TEST_DIR, LONG_SPEC_TEST_DIR, timeoutMilliseconds );
      // Proof it 
      Assert.True(SlackerResults.PassedSpecs > 0);
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }


  }
}

