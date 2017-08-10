﻿using System;
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
    public static string SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec") + "/");


    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void runAllSpecsInDirectory()
    {
      // Use explicit timeout as it's running all the tests in the spec directory
      int timeoutMilliseconds = 100 * 1000;
      // Run all the tests in the directory at once 
      SlackerResults SlackerResults = new SlackerService().RunDirectory(RUN_TEST_DIR, SPEC_TEST_DIR, timeoutMilliseconds );
      // Proof it - got a few passed tests ( see debug for Slacker output )
      Assert.True(SlackerResults.PassedSpecs > 7, SlackerResults.Message );
    }

    [Fact]
    public void timeout()
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


  }
}

