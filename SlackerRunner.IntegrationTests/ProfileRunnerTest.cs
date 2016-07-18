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
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb");
      });
      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True( ex.Message.IndexOf("The file does not exist, file=") > -1 );
    }

    // Testing faliure
    [Fact]
    public void DirectoryNotFound()
    {
      Exception ex = Record.Exception(() =>
      {
        SlackerResults SlackerResults = new SlackerService().Run( "does not exist", SpecsTester.SPEC_TEST_DIR + @"sample\filedoesnotexist.rb");
      });
      // Proof 
      // Check the Exception thrown 
      Assert.NotNull(ex);
      Assert.True(ex is SlackerException);
      Assert.True(ex.Message.IndexOf("The directory does not exist, directory=") > -1);
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
    [Fact(Skip ="Activate by hand when neeeded, to test failure bahaviour")]  
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

    [Fact(Skip = "Acivate by hand when neeeded, to test slacker not found exception")]
    //[Fact]
    public void TestSlackerMissing()
    {
      // The directory that the slacker resides in
      String testDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(SpecTestFile).Assembly.Location) ));
      
      // Wrap the Exception 
      Exception ex = Record.Exception( () =>
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