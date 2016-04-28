using System;
using System.IO;
//
using Xunit;
using Xunit.Extensions;
//
using SlackerRunner;



namespace SlackerRunner.IntegrationTests
{
    public class SpecsTester
    {
        // Relative path to test dir 
        public static string RUN_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests")) + "\\";
        public static string SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec" ) + "/");
        public static string LONG_SPEC_TEST_DIR = Path.GetFullPath(Path.Combine("..", "..", "..", "SlackerTests", "spec", "some_long_folder") + "/");
        /// <summary>
        /// Runs all the Slacker spec tests in the some_long_folder 
        /// </summary>
        /// <param name="rbFile"></param>
        [Theory, ClassData(typeof(SpecsTesterResolver))]
        public void runSpecs(SpecTestFile rbFile)
        {
            SlackerResults SlackerResults = new SlackerService().Run(RUN_TEST_DIR, LONG_SPEC_TEST_DIR + rbFile.FileName );
            Assert.True(SlackerResults.Passed, "Test should have succeeded.");
        }

    }
}

