using System;
using System.IO;
//
using Xunit;
//
using SlackerRunner.Util;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace SlackerRunner.IntegrationTests
{
  public class SpecsDirectoryTester
  {
    // Relative path to test dir 
    public static string RUN_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests")) + "\\";
    public static string SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec") + "/");


    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunAllSpecsInDirectory()
    {
      // Use explicit timeout as it's running all the tests in the spec directory
      int timeoutMilliseconds = 100 * 1000;
      // Run all the tests in the directory at once 
      SlackerResults SlackerResults = new SlackerService().RunDirectory(RUN_TEST_DIR, SPEC_TEST_DIR, timeoutMilliseconds );
      // Proof it - got a few passed tests ( see debug for Slacker output )
      Assert.True(SlackerResults.PassedSpecs > 7, SlackerResults.Message );
    }

    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunAllSpecsInSubDirectory()
    {
      // Use explicit timeout as it's running all the tests in the spec directory
      int timeoutMilliseconds = 100 * 1000;
      // Run all the tests in the directory at once 
      SlackerResults SlackerResults = new SlackerService().RunDirectory(RUN_TEST_DIR, SPEC_TEST_DIR + "sample", timeoutMilliseconds);
      // Proof it - got a few passed tests ( see debug for Slacker output )
      Assert.True(SlackerResults.PassedSpecs > 3, SlackerResults.Message);
    }

    [Fact]
    public void Timeout()
    {
      // Test timeout by setting very small timeout threshold
      int timeoutMilliseconds = 1;
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().RunDirectory(RUN_TEST_DIR, SPEC_TEST_DIR, timeoutMilliseconds);
      });

      // Exception was thrown
      Assert.NotNull(ex);
      Assert.True(ex.ToString().IndexOf("timeout") > -1 );
    }


    [Fact]
    public void TimeoutMulti()
    {
      // Test timeout by setting very small timeout threshold
      int timeoutMilliseconds = 1;
      Exception ex = Record.Exception(() =>
      {
        IEnumerable<SlackerResults> SlackerResults = new SlackerService().RunDirectoryMultiResults(RUN_TEST_DIR, SPEC_TEST_DIR, timeoutMilliseconds);
      });

      // Exception was thrown
      Assert.NotNull(ex);
      Assert.True(ex.ToString().IndexOf("timeout") > -1);
    }


    // DisableDiscoveryEnumeration option has to be set to true, otherwise 
    // the GetResults function will be run twice, once during discovery
    // and once during the actual test run
    [Theory(Skip = "Live database needed"), MemberData("GetResultsDirectoryNotThere", DisableDiscoveryEnumeration = true)]
    //[Theory, MemberData("GetResultsDirectoryNotThere", DisableDiscoveryEnumeration = true)]
    public void RunAllSpecsInSubDirectoryMultipleResultsDirNotPresent(SlackerResults File)
    {
      // Proof it check each one 
      Assert.False(File.Passed, File.Trace);
    }

    public static IEnumerable<object[]> GetResultsDirectoryNotThere()
    {
      // Use explicit timeout as it's running all the tests in the spec directory
      int timeoutMilliseconds = 1 * 60 * 1000;
      // Run all the tests in the directory at once 
      IEnumerable<SlackerResults> multi = new SlackerService().RunDirectoryMultiResults(RUN_TEST_DIR, SPEC_TEST_DIR + "fake directory", timeoutMilliseconds);

      // Yeild results 
      foreach (SlackerResults res in multi)
        yield return new object[] { res };
    }

    // DisableDiscoveryEnumeration option has to be set to true, otherwise 
    // the GetResults function will be run twice, once during discovery
    // and once during the actual test run
    [Theory(Skip = "Live database needed"), MemberData("GetResults", DisableDiscoveryEnumeration = true)]
    //[Theory, MemberData("GetResults", DisableDiscoveryEnumeration = true)]
    public void RunAllSpecsInSubDirectoryMultipleResults(SlackerResults File)
    {
      // Proof it check each one 
      Assert.True(File.Passed, File.Trace);
    }

    public static IEnumerable<object[]> GetResults()
    {
      // Use explicit timeout as it's running all the tests in the spec directory
      int timeoutMilliseconds = 1 * 60 * 1000;
      // Run all the tests in the directory at once 
      IEnumerable<SlackerResults> multi = new SlackerService().RunDirectoryMultiResults(RUN_TEST_DIR, SPEC_TEST_DIR, timeoutMilliseconds);

      // Yeild results 
      foreach( SlackerResults res in multi )
        yield return new object[] { res };
    }
    
  }
}

