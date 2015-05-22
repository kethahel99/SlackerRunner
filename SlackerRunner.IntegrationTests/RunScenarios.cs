using System.ComponentModel;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackerRunner.IntegrationTests
{
    //Add a category for each build you want to be notified of in TFS e.g. SmokeTestLive and create a matching profile in Slacker.yml to control which 
    //features are run
    [TestClass]
    public class RunScenarios
    {

        private static string testPath = Path.Combine("..", "..", "..", "SlackerTests");

        [TestMethod, Category("SmokeTestLive")]
        public void RunWithPassingProfileForSmokeTestLiveBuildReturnsTrue()
        {
            string testDirectory = Path.GetFullPath(testPath);
            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", @".\spec\sample\sample1.rb", "testoutput.txt");
            Assert.IsTrue(SlackerResults.Passed, "Smoke tests failed for live, see smokeTestLive.txt for details.");
        }

        [TestMethod, Category("SmokeTestLive"), ExpectedException(typeof(Win32Exception))]
        public void RunWithPassingProfileForSmokeTestLiveBuildErrorsWithInvalidUser()
        {
            string testDirectory = Path.GetFullPath(testPath);
            var user = new User { Domain = "InvalidDomain", Name = "InvalidName", Password = "InvalidPassword" };
            new SlackerService().Run(testDirectory, "run.bat", "passingProfile", "testoutput.txt", user);
        }
    }
}