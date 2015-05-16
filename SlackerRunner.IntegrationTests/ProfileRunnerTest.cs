using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackerRunner.IntegrationTests
{
    [TestClass]
    public class ProfileRunnerTest
    {

        private static string testPath = Path.Combine("..", "..", "..", "SlackerTests");

        [TestMethod]
        public void RunWithPassingProfileReturnsTrue()
        {
            string testDirectory = Path.GetFullPath(testPath);

            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", "passingProfile", "output.txt");

            Assert.IsTrue(SlackerResults.Passed, "Passing test not picked up.");
        }

        [TestMethod]
        public void RunWithFailingProfileReturnsFalse()
        {
            string testDirectory = Path.GetFullPath(testPath);

            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", "failingProfile", "output.txt");

            Assert.IsFalse(SlackerResults.Passed, "Failing test not picked up.");
        }

        [TestMethod]
        public void RunWithDefaultProfileReturnsFalse()
        {
            string testDirectory = Path.GetFullPath(testPath);

            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", "defaultProfile", "output.txt");

            Assert.IsFalse(SlackerResults.Passed, "Failing test not picked up.");
        }
    }

}