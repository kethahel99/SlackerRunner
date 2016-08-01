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
    [Fact, Category("SmokeTestLive")]
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
      // Assert.Equal( "The user name or password is incorrect", exception.Message );
    }

    [Fact(Skip ="NotReady")]
    public void CheckSlackerExitCode()
    {
      var startInfo = new ProcessStartInfo("slacker");
      startInfo.WorkingDirectory = @"C:\work\_solvas\put slacker runner here\SlackerRunner\SlackerTests\";
      Process proc = Process.Start(startInfo);
      proc.WaitForExit();
      Assert.True( proc.ExitCode != 0);
    }
  }
}