using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using Xunit;

namespace SlackerRunner.IntegrationTests
{
  //Add a category for each build you want to be notified of in TFS e.g. SmokeTestLive and create a matching profile in Slacker.yml to control which 
  //features are run
  public class RunScenarios
  {
    [Fact(Skip = "Live database needed")]
    //[Fact]
    public void RunWithPassingProfileForSmokeTestLiveBuildReturnsTrue()
    {
      SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb");
      Assert.True(SlackerResults.Passed, SlackerResults.Message);
    }

    [Fact]
    public void RunWithPassingProfileForSmokeTestLiveBuildErrorsWithInvalidUser()
    {
      string testDirectory = Path.GetFullPath(SpecsTester.RUN_TEST_DIR);
      var user = new User { Domain = "InvalidDomain", Name = "InvalidName", Password = "InvalidPassword" };
      var exception = Record.Exception(() =>
      {
        new SlackerService().Run(testDirectory, "passingProfile", user);
      });
      Assert.IsAssignableFrom<Win32Exception>(exception);
    }


    // See -- https://github.com/vassilvk/slacker/issues/3
    [Fact(Skip = "Acivate by hand when neeeded to test slacker non zero exit code")]
    //[Fact]
    public void CheckSlackerExitCode()
    {
      // Setup
      var startInfo = new ProcessStartInfo("slacker");
      startInfo.WorkingDirectory = @"C:\work\_solvas\put slacker runner here\SlackerRunner\SlackerTests\";
      // Run without popping up the cmd window
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      // Start 
      Process proc = Process.Start(startInfo);
      proc.WaitForExit();
      Assert.True( proc.ExitCode != 0);
    }
  }
}