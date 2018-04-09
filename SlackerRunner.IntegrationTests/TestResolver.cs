using System;
using System.IO;
using System.Collections.Generic;
//
using Xunit;
//
using SlackerRunner.Util;


namespace SlackerRunner.IntegrationTests
{
  public class TestResolver
  {

    /// <summary>
    /// Excersizes the SpecTesterResolver and SpecTestFile
    /// </summary>
    [Theory, MemberData(nameof(TestFiles), typeof(SpecTestFile))]
    public void SmokeRunSpecs(ISpecTestFile File)
    {
      Assert.NotNull(File.FileName);
    }


    /// <summary>
    /// Excersizes the SpecTesterResolver and IndividualSpecTestFile
    /// </summary>
    [Theory, MemberData(nameof(TestFiles), typeof(IndividualSpecTestFile))]
    public void SmokeRunSpecsIndividually(ISpecTestFile File)
    {
      Assert.NotNull(File.FileName);
    }

    /// <summary>
    /// Uses the SpecTesterResolver to figure out all the test files in a directory
    /// </summary>
    public static IEnumerable<object[]> TestFiles(Type type)
    {
      // Pass either SpecTestFile to run tests in a group or IndividualTestFile to run one test file at a time 
      List<ISpecTestFile> files = Util.SpecsTesterResolver.ProcessDirectory(SpecsTester.SPEC_TEST_DIR, type);

      // Back to caller
      foreach (ISpecTestFile file in files)
        yield return new object[] { file };
    }

  }
}

