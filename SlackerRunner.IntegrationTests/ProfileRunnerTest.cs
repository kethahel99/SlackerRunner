//
using System;
using System.IO;
using Xunit;


namespace SlackerRunner.IntegrationTests
{

  public class ProfileRunnerTest
  {

    // Testing faliure
    [Fact]
    public void FileNotFound()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb", "testoutput.txt");
      // Proof 
      Assert.False(SlackerResults.Passed, "Test should have failed.");
      Assert.True(SlackerResults.FailedSpecs == 1);
      Assert.False(SlackerResults.PassedSpecs > 0);
    }

    [Fact]
    public void TestFileSample1()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb", "testoutput.txt");
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }

    [Fact]
    public void TestFileSample1DirWithSpace()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sam ple\sample1.rb", "testoutput.txt");
      // Proof
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }


    // Testing failure 
    [Fact]
    public void TestFileNotPassing2()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sample\sample2.rb", "testoutput.txt");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 4);
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 2);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, "Test should NOT have succeeded.");
    }

    // Testing failure 
    [Fact]
    public void TestFileNotPassing3()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sample\sample3.rb", "testoutput.txt");
      // Proof it, 4 failures
      Assert.True(SlackerResults.FailedSpecs == 2);
      // and two passed
      Assert.True(SlackerResults.PassedSpecs == 0);
      // Overall not passed 
      Assert.False(SlackerResults.Passed, "Test should NOT have succeeded.");
    }

    [Fact]
    public void TestLongSubDirName()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"some_long_folder\below_that_long_folder_yet\and_this_one_longer_yet_for_long_name_testing\smpl.rb", "testoutput.txt");
      // Proof it
      Assert.True(SlackerResults.PassedSpecs == 2);
      Assert.True(SlackerResults.Passed, "Test should have succeeded.");
    }
  }

}