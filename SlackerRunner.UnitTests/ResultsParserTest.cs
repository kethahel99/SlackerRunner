using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackerRunner.UnitTests
{
    [TestClass]
    public class ResultsParserTest
    {
        private static string ResultSomeFailed()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Scenario:");
            sb.AppendLine("   ");
            sb.AppendLine("1027 scenarios (1 failed, 1026 passed)");
            sb.AppendLine("8 steps (5 failed, 3 passed)");
            sb.AppendLine("0m0.015s");
            sb.AppendLine("");
            return sb.ToString();
        }

        private static string ResultAllPassed()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Scenario:");
            sb.AppendLine("   ");
            sb.AppendLine("10 scenarios (10 passed)");
            sb.AppendLine("987 steps (987 passed)");
            sb.AppendLine("0m0.015s");
            sb.AppendLine("");
            return sb.ToString();
        }

        [TestMethod]
        public void ParseReturnsFailedScenariosWhenAllPassed()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), null);
            Assert.AreEqual(SlackerResults.FailedScenarios, 0);
            Assert.AreEqual(SlackerResults.PassedScenarios, 10);
            Assert.AreEqual(SlackerResults.Scenarios, 10);
            Assert.AreEqual(SlackerResults.Steps, 987);
        }

        [TestMethod]
        public void ParseReturnsFailedScenariosWhenSomeFailed()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultSomeFailed(), null);
            Assert.AreEqual(SlackerResults.FailedScenarios, 1);
            Assert.AreEqual(SlackerResults.PassedScenarios, 1026);
            Assert.AreEqual(SlackerResults.Scenarios, 1027);
            Assert.AreEqual(SlackerResults.Steps, 8);
        }

        [TestMethod]
        public void ParseReturnsFalseWhenStandardErrorNotNull()
        {
            var resultsParser = new ResultsParser();

            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "StandardError");

            Assert.IsFalse(SlackerResults.Passed);
        }

        [TestMethod]
        public void ValidateReturnsFalseWhenOutResultsHaveFailedScenario()
        {
            var resultsParser = new ResultsParser();

            SlackerResults SlackerResults = resultsParser.Parse(ResultSomeFailed(), "StandardError");

            Assert.IsFalse(SlackerResults.Passed);
        }

        [TestMethod]
        public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsEmpty()
        {
            var resultsParser = new ResultsParser();

            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "");

            Assert.IsTrue(SlackerResults.Passed);
        }

        [TestMethod]
        public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsNull()
        {
            var resultsParser = new ResultsParser();

            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), null);

            Assert.IsTrue(SlackerResults.Passed);
        }
    }
}