using System.Text.RegularExpressions;

namespace SlackerRunner
{
    public class ResultsParser : IResultsParser
    {
        readonly Regex scenarios = new Regex(@"(?<scenarios>\d+)\sscenarios", RegexOptions.Compiled);
        readonly Regex steps = new Regex(@"(?<steps>\d+)\ssteps", RegexOptions.Compiled);
        readonly Regex failedScenarios = new Regex(@"(?<failed>\d+)\sfailed", RegexOptions.Compiled);
        readonly Regex passedScenarios = new Regex(@"(?<passed>\d+)\spassed", RegexOptions.Compiled);

        public SlackerResults Parse(string result, string standardError)
        {
            return new SlackerResults
            {
                Scenarios = Find("scenarios", result, scenarios),
                Steps = Find("steps", result, steps),
                FailedScenarios = Find("failed", result, failedScenarios),
                PassedScenarios = Find("passed", result, passedScenarios),
                Passed = Find("failed", result, failedScenarios) == 0 && string.IsNullOrEmpty(standardError)
            };
        }

        private static int Find(string group, string result, Regex regex)
        {
            string match = regex.Match(result).Groups[group].Value;
            return string.IsNullOrEmpty(match) ? 0 : int.Parse(match);
        }
    }
}