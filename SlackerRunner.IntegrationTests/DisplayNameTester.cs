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
  public class DisplayNameTester
  {

    /// <summary>
    /// Excersize SlackerRunnerFact
    /// </summary>
    [SlackerRunnerFact]
    public void SlackerRunnerFactTest()
    {
      Assert.True(true);
    }

    /// <summary>
    /// Excersize SlackerRunnerFact
    /// </summary>
    [Fact]
    public void TraditionalFactTest()
    {
      Assert.True(true);
    }

    /// <summary>
    /// Excersize SlackerRunnerTheory
    /// </summary>
    [Theory, MemberData("TestFiles", typeof(IndividualSpecTestFile))]
    public void TraditionalRunSpecFiles(ISpecTestFile File)
    {
      Assert.True(File != null);
    }


    /// <summary>
    /// Excersize SlackerRunnerTheory
    /// </summary>
    [SlackerRunnerTheory, MemberData("TestFiles", typeof(IndividualSpecTestFile))]
    public void RunSpecFiles(ISpecTestFile File)
    {
      Assert.True(File != null);
    }

    /// <summary>
    /// Uses the SpecTesterResolver to figure out all the test files in a directory
    /// </summary>
    public static IEnumerable<object[]> TestFiles(Type type)
    {
      // Back to caller
      IndividualSpecTestFile file = new IndividualSpecTestFile();
      file.FileName = "TestFileName";
      yield return new object[] { file };
    }

  }
}

