//
using System;
using System.Text;
//
using Xunit;


namespace SlackerRunner.UnitTests
{
    public class ResultsParserTest
    {
        private static string ResultSomeFailed()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Beachcomber ((local))");
            sb.AppendLine("............FFF............");
            sb.AppendLine(" ");
            sb.AppendLine("Finished in 0.11845 seconds");
            sb.AppendLine("24 examples, 3 failure");
            sb.AppendLine("");
            return sb.ToString();
        }


        private static string ResultOnePassed()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Beachcomber ((local))");
            sb.AppendLine("...");
            sb.AppendLine(" ");
            sb.AppendLine("Finished in 0.11845 seconds");
            sb.AppendLine("1 example, 0 failures");
            sb.AppendLine("");
            return sb.ToString();
        }

        private static string ResultAllPassed()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Beachcomber ((local))");
            sb.AppendLine("...");
            sb.AppendLine(" ");
            sb.AppendLine("Finished in 0.11845 seconds");
            sb.AppendLine("3 examples, 0 failures");
            sb.AppendLine("");
            return sb.ToString();
        }

        private static string UnforseenError()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Beachcomber ((local))");
            sb.AppendLine("...");
            sb.AppendLine(" ");
            sb.AppendLine("ODBC::Error: 28000 (18456) [Microsoft][ODBC SQL Server Driver][SQL Server]Login failed for user 'something'.");
            sb.AppendLine("");
            return sb.ToString();
        }

        [Fact]
        public void ParseUnforseenError()
        {
            var resultsParser = new ResultsParser();
            SlackerResults res = resultsParser.Parse(UnforseenError(), null);
            // advertise what what the score is 
            Console.WriteLine(res.getString());
            // Proof it 
            Assert.True(res.FailedSpecs > 0);
            Assert.False( res.Passed );
        }

        [Fact]
        public void ParseReturnsFailedSpecsWhenAllPassed()
        {
            var resultsParser = new ResultsParser();
            SlackerResults res = resultsParser.Parse(ResultAllPassed(), null);
            // advertise what what the score is 
            Console.WriteLine(res.getString());
            // Proof it 
            Assert.Equal(res.FailedSpecs, 0);
            Assert.Equal(res.PassedSpecs, 3);
        }

        [Fact]
        public void ParseReturnsFailedSpecsWhenOnePassed()
        {
            var resultsParser = new ResultsParser();
            SlackerResults res = resultsParser.Parse(ResultOnePassed(), null);
            // advertise what what the score is 
            Console.WriteLine(res.getString());
            // Proof it 
            Assert.Equal(res.FailedSpecs, 0);
            Assert.Equal(res.PassedSpecs, 1);
        }


        // This is the main parser test 
        [Fact]
        public void ParseReturnsFailedSpecsWhenSomeFailed()
        {
            var resultsParser = new ResultsParser();
            SlackerResults res = resultsParser.Parse(ResultSomeFailed(), null);
            // advertise what what the score is 
            Console.WriteLine(res.getString());
            // Proof it 
            Assert.Equal(res.FailedSpecs, 3);
            Assert.Equal(res.PassedSpecs, 21);
        }

        [Fact]
        public void ParseReturnsFalseWhenStandardErrorNotNull()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "StandardError");
            Assert.False(SlackerResults.Passed);
        }

        [Fact]
        public void ValidateReturnsFalseWhenOutResultsHaveFailedScenario()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultSomeFailed(), "StandardError");
            Assert.False(SlackerResults.Passed);
        }

        [Fact]
        public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsEmpty()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), "");
            Assert.True(SlackerResults.Passed);
        }

        [Fact]
        public void ValidateReturnsTrueWhenOutputResultsPassedAndErrorIsNull()
        {
            var resultsParser = new ResultsParser();
            SlackerResults SlackerResults = resultsParser.Parse(ResultAllPassed(), null);
            Assert.True(SlackerResults.Passed);
        }
    }
}