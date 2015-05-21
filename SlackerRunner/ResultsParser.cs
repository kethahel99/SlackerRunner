//
using System;
using System.Text.RegularExpressions;


namespace SlackerRunner
{
    public class ResultsParser : IResultsParser
    {
        readonly Regex scenarios = new Regex(@"(?<scenarios>\d+)\sscenarios", RegexOptions.Compiled);
        readonly Regex steps = new Regex(@"(?<seconds>\d+(.\d+)?)\sseconds", RegexOptions.Compiled);
        readonly Regex FailedSpecs = new Regex(@"(?<failures>\d+)\sfailures", RegexOptions.Compiled);
        readonly Regex PassedSpecs = new Regex(@"(?<examples>\d+)\sexamples", RegexOptions.Compiled);

        public SlackerResults Parse(string result, string standardError)
        {
            return new SlackerResults
            {
                What = getFirst(result),
                Seconds = FindDouble("seconds", result, steps),
                FailedSpecs = FindInt("failures", result, FailedSpecs),
                PassedSpecs = FindInt("examples", result, PassedSpecs) - FindInt("failures", result, FailedSpecs),
                Passed = FindInt("failures", result, FailedSpecs) == 0 && string.IsNullOrEmpty(standardError)
            };
        }

        /// <summary>
        /// Returns the first line that describes what was run
        /// </summary>
        private static string getFirst(string result)
        {
            int where = result.IndexOf(Environment.NewLine);
            return result.Substring(0, where - 1);
        }


        /// <summary>
        /// Returns the results as int 
        /// </summary>
        private static int FindInt(string group, string result, Regex regex)
        {
            string match = regex.Match(result).Groups[group].Value;
            return string.IsNullOrEmpty(match) ? 0 : int.Parse(match);
        }
        
        /// <summary>
        /// Returns the result as double 
        /// </summary>
        private static double FindDouble(string group, string result, Regex regex)
        {
            string match = regex.Match(result).Groups[group].Value;
            return string.IsNullOrEmpty(match) ? 0 : double.Parse(match);
        }
    }
}