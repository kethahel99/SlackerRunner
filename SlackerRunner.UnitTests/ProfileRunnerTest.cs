using Xunit;
using Rhino.Mocks;

namespace SlackerRunner.UnitTests
{
    public class ProfileRunnerTest
    {
        private const string Profile = "profile";
        private const string StandardOutput = "Run Slacker";
        private const string StandardError = "Some Error Message";
        private const string TestDirectory = "F:/Tests";
        private const string BatchFileName = "run.bat";
        private const string OutputFileName = "output.txt";

        [Fact]
        public void RunCallsProcessRunnerAndResultsValidatorAndSetsPassedToTrueWhenValid()
        {
            var SlackerResults = new SlackerResults();
            var processRunner = MockRepository.GenerateMock<IProcessRunner>();
            var resultsParser = MockRepository.GenerateStub<IResultsParser>();
            processRunner.Expect(x => x.Run(TestDirectory, BatchFileName, Profile, OutputFileName));
            processRunner.Expect(x => x.StandardError).Return(StandardError).Repeat.Any();
            processRunner.Expect(x => x.StandardOutput).Return(StandardOutput);
            resultsParser.Expect(x => x.Parse(TestDirectory + OutputFileName, StandardError)).Return(SlackerResults);

            var SlackerService = new ProfileRunner(processRunner, resultsParser, FileCreator, ReadOutputFile, LogResults);
            SlackerResults SlackerResultsReturned = SlackerService.Run(TestDirectory, BatchFileName, Profile, OutputFileName);

            Assert.Same(SlackerResults, SlackerResultsReturned);
            processRunner.VerifyAllExpectations();
        }

        private static void FileCreator(string testDirectory, string outputFileName)
        {
            Assert.Equal(testDirectory, TestDirectory);
            Assert.Equal(outputFileName, OutputFileName);
        }


        private static void LogResults(string testDirectory, string outputFileName, string standardError, string standardOutput)
        {
            Assert.Equal(testDirectory, TestDirectory);
            Assert.Equal(outputFileName, OutputFileName);
            Assert.Equal(standardError, StandardError);
            Assert.Equal(standardOutput, StandardOutput);
        }

        private static string ReadOutputFile(string testDirectory, string outputFileName)
        {
            Assert.Equal(testDirectory, TestDirectory);
            Assert.Equal(outputFileName, OutputFileName);

            return TestDirectory + OutputFileName;
        }
    }
}