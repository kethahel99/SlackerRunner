//
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SlackerRunner.IntegrationTests
{
    [TestClass]
    public class ProfileRunnerTest
    {
        // Relative path to test dir 
        private static string testPath = Path.Combine("..", "..", "..", "SlackerTests");
        

        [TestMethod] 
        public void FileNotFound()
        {
            string testDirectory = Path.GetFullPath(testPath);
            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", @".\spec\sample\filedoesnotexist.rb", "testoutput.txt");
            Assert.IsFalse(SlackerResults.Passed, "Test should have failed.");
        }

        [TestMethod]
        public void TestFileSample1()
        {
            string testDirectory = Path.GetFullPath(testPath);
            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", @".\spec\sample\sample1.rb", "testoutput.txt");
            Assert.IsTrue(SlackerResults.Passed, "Test should have succeeded.");
        }

        [TestMethod]
        public void TestFileSample2()
        {
            string testDirectory = Path.GetFullPath(testPath);
            SlackerResults SlackerResults = new SlackerService().Run(testDirectory, "run.bat", @".\spec\sample\sample2.rb", "testoutput.txt");
            // Proof it, 4 failures
            Assert.IsTrue( SlackerResults.FailedSpecs == 4);
            Assert.IsFalse( SlackerResults.Passed, "Test should NOT have succeeded.");
        }
    }

}