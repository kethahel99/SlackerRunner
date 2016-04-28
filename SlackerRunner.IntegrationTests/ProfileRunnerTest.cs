//
using System;
using System.IO;
using System.Reflection;
using Xunit;


namespace SlackerRunner.IntegrationTests
{

  public class ProfileRunnerTest
  {

    // Testing faliure
    [Fact]
    public void FileNotFound()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb");
      // Proof 
      Assert.False(SlackerResults.Passed, "Test should have failed.");
      Assert.True(SlackerResults.FailedSpecs == 1);
      Assert.False(SlackerResults.PassedSpecs > 0);
    }

    [Fact]
    public void TestFileSample1()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb" );
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }

    [Fact]
    public void TestFileSample1DirWithSpace()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sam ple\sample1.rb" );
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }


    // Testing failure 
    [Fact]
    public void TestFileNotPassing2()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample2.rb");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 4);
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 2);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, "Test should NOT have succeeded.");
    }

    // Testing failure 
    //[Fact]
    [Fact(Skip ="Acivate by hand when neeeded, to test failure bahaviour")]  
    public void TestFileNotPassing3()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 0 );
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 0);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, "Test should NOT have succeeded.");
    }


    // the runner has to throw SlackerException 
    [Fact]
    public void TestRunBatMissing()
    {
      // The directory that the slacker resides in
      String testDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(SpecTestFile).Assembly.Location) ));

      // Wrap the Exception 
      Exception ex = Record.Exception(new Assert.ThrowsDelegate(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run(testDir, SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb");
      }));

      // Check the Exception thrown 
      Assert.True(ex is SlackerException);
    }

    [Fact]
    public void TestLongSubDirName()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"some_long_folder\below_that_long_folder_yet\and_this_one_longer_yet_for_long_name_testing\smpl.rb" );
      // Proof it
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }
  }

}