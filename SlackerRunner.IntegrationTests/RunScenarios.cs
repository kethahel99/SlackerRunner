using System.ComponentModel;
using System.IO;
using Xunit;

namespace SlackerRunner.IntegrationTests
{
    //Add a category for each build you want to be notified of in TFS e.g. SmokeTestLive and create a matching profile in Slacker.yml to control which 
    //features are run
    public class RunScenarios
    {

        [Fact, Category("SmokeTestLive")]
        public void RunWithPassingProfileForSmokeTestLiveBuildReturnsTrue()
        {
            SlackerResults SlackerResults = new SlackerService().Run(SpecsTester.RUN_TEST_DIR, "run.bat", SpecsTester.SPEC_TEST_DIR + @"sample\sample1.rb", "testoutput.txt");
            Assert.True(SlackerResults.Passed, "Smoke tests failed for live, see smokeTestLive.txt for details.");
        }
        
        /*
        [Fact, Category("SmokeTestLive"), ExpectedException(typeof(Win32Exception))]
        public void RunWithPassingProfileForSmokeTestLiveBuildErrorsWithInvalidUser()
        {
            string testDirectory = Path.GetFullPath(testPath);
            var user = new User { Domain = "InvalidDomain", Name = "InvalidName", Password = "InvalidPassword" };
            new SlackerService().Run(testDirectory, "run.bat", "passingProfile", "testoutput.txt", user);
        }
        */
    }
}