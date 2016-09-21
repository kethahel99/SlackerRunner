using System;
using System.IO;
using Xunit;
using SlackerRunner.IntegrationTests.Util;

namespace SlackerRunner.IntegrationTests
{
  public class SpecsTester
  {
    // Relative path to test dir 
    public static string RUN_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests")) + "\\";
    public static string SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec" ) + "/");
    public static string LONG_SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec", "sam ple") + "/");

    /// <summary>
    /// Runs all the Slacker spec tests in the LONG_SPEC_TEST_DIR
    /// </summary>
    [Theory, ClassData(typeof(SpecsTesterResolver))]
    public void runSpecs(SpecTestFile rbFile)
    {
      SlackerResults SlackerResults = new SlackerService().Run(RUN_TEST_DIR, LONG_SPEC_TEST_DIR + rbFile.FileName );
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }

    /// <summary>
    /// Runs all the Slacker spec tests in the LONG_SPEC_TEST_DIR folder as individual tests 
    /// </summary>
    [Theory, ClassData(typeof(IndividualSpecsTesterResolver))]
    public void runSpecsIndividually(IndividualSpecTestFile rbFile)
    {
      SlackerResults SlackerResults = new SlackerService().Run(RUN_TEST_DIR, LONG_SPEC_TEST_DIR + rbFile.FileName);
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }


  }
}

