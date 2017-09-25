using System;
using System.IO;
using System.Collections.Generic;
//
using Xunit;
//
using SlackerRunner.Util;
using SlackerRunner.Xunit;


namespace SlackerRunner.IntegrationTests
{
  public class SpecsTester
  {
    // Relative path to test dir 
    public static string RUN_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests")) + "\\";
    public static string SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec") + "/");
    public static string LONG_SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec", "sam ple") + "/");


    /// <summary>
    /// Runs Slacker spec tests 
    /// </summary>
    [Theory(Skip = "Live database needed"), MemberData("TestFiles", typeof(SpecTestFile))]
    //[Theory, MemberData("TestFiles", typeof(SpecTestFile))]
    public void RunSpecs(ISpecTestFile File)
    {
      SlackerResults SlackerResults = new SlackerService().Run(RUN_TEST_DIR, LONG_SPEC_TEST_DIR + File.FileName);
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }

    /// <summary>
    /// Runs Slacker spec tests 
    /// </summary>
    [SlackerRunnerTheory(Skip = "Live database needed"), MemberData("TestFiles", typeof(IndividualSpecTestFile))]
    //[SlackerRunnerTheory, MemberData("TestFiles", typeof(IndividualSpecTestFile))]
    public void RunSpecsIndividually(ISpecTestFile File)
    {
      SlackerResults SlackerResults = new SlackerService().Run(RUN_TEST_DIR, LONG_SPEC_TEST_DIR + File.FileName);
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }

    /// <summary>
    /// Uses the SpecTesterResolver to figure out all the test files in a directory
    /// </summary>
    public static IEnumerable<object[]> TestFiles(Type type)
    {
      // Pass either SpecTestFile to run tests in a group or IndividualTestFile to run one test file at a time 
      List<ISpecTestFile> files = Util.SpecsTesterResolver.ProcessDirectory(LONG_SPEC_TEST_DIR, type);

      // Back to caller
      foreach (ISpecTestFile file in files)
        yield return new object[] { file };
    }

  }
}

