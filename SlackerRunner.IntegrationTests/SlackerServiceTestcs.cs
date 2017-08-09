using System;
using System.IO;
using Xunit;
using System.ComponentModel;
using SlackerRunner.Util;

namespace SlackerRunner.IntegrationTests
{
  public class SlackerServiceTest
  {
    // Testing failure
    [Fact(Skip = "File not exist is on hold, Fixit")]
    //[Fact]
    public void FileNotFound()
    {
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb");
      });
      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True(ex.Message.IndexOf("The file does not exist, file=") > -1);
    }

    // Testing failure
    [Fact]
    public void DirectoryNotFound()
    {
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run("does not exist", SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb");
      });
      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True(ex.Message.IndexOf("The directory does not exist, directory=") > -1);
    }

    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void TestFileSample1()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb");
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2, SlackerResults.Message);
    }

    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void TestFileSample1DirWithSpace()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sam ple\sample1.rb");
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2, SlackerResults.Message);
    }

    // Testing failure 
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void TestFileNotPassing2()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample2.rb");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 4, SlackerResults.Message);
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 2, SlackerResults.Message);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, SlackerResults.Message);
    }

    // Testing failure 
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void TestFileNotPassing3()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 2, SlackerResults.Message);
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 0, SlackerResults.Message);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, SlackerResults.Message);
    }

    [Fact(Skip = "Acivate by hand when neeeded to test slacker not found exception")]
    //[Fact]
    public void TestSlackerMissing()
    {
      // The directory that the slacker resides in
      String testDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(SpecTestFile).Assembly.Location)));

      // Wrap the Exception 
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run(testDir, SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb");
        //Assert.NotNull(SlackerResults);
      });

      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
    }

    [Fact]
    public void Repro()
    {
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run("", SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb");
      });
      Assert.NotNull(ex);
    }

    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void TestLongSubDirName()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"some_long_folder\below_that_long_folder_yet\and_this_one_longer_yet_for_long_name_testing\smpl.rb");
      // Proof it
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }

    // Testing RunDirectory, directory not found 
    [Fact]
    public void DirectoryNotFoundRunDir()
    {
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().RunDirectory("does not exist", "not this one either", 10000);
      });
      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True(ex.Message.IndexOf("The directory does not exist, directory=") > -1);
    }

    // Smoke test contructor
    [Fact]
    public void SmokeTest()
    {
      var service = new SlackerService( 1000 );
      // Proof
      Assert.NotNull(service);
      Assert.True(service.TimeoutMilliseconds == 1000);
      // Further timeout testing
      service.TimeoutMilliseconds = 1500;
      Assert.True(service.TimeoutMilliseconds == 1500);
      
    }

  }
}