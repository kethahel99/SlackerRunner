//
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace SlackerRunner
{
    public class ResultsParser : IResultsParser
    {
        // Regexs 
        const string _FAILURE = "failure";
        readonly Regex seconds = new Regex(@"(?<seconds>\d+(.\d+)?)\sseconds", RegexOptions.Compiled);
        readonly Regex FailedSpecs = new Regex(@"(?<failure>\d+)\sfailure", RegexOptions.Compiled);
        readonly Regex PassedSpecs = new Regex(@"(?<examples>\d+)\sexamples", RegexOptions.Compiled);
        //
        private SlackerResults _res = new SlackerResults();

        
        /// <summary>
        /// Parses Passed, failures from the output results into SlackerResults
        /// </summary>
        /// <param name="result"></param>
        /// <param name="standardError"></param>
        /// <returns></returns>
        public SlackerResults Parse(string result, string standardError)
        {
            // Extract results 
            _res = new SlackerResults
            {
                Header = getLine(result, 1),
                Trace = getLine(result, 2),
                Seconds = FindDouble("seconds", result, seconds),
                FailedSpecs = FindInt(_FAILURE, result, FailedSpecs),
                PassedSpecs = FindInt("examples", result, PassedSpecs) - FindInt(_FAILURE, result, FailedSpecs),
                Passed = FindInt(_FAILURE, result, FailedSpecs) == 0 && string.IsNullOrEmpty(standardError)
            };

            // 
            return _res;
        }

        /// <summary>
        /// Returns the given line
        /// </summary>
        private static string getLine(string result, int lineNumber )
        {
            int where = 0;
            int last= 0;
            int count = 0;
            
            // Retrive the line asked for, cut out line endings 
            while ( count != lineNumber)
            {
                last = where;
                where = result.IndexOf(Environment.NewLine, where  );
                where += Environment.NewLine.Length;
                count++;
            }
            return result.Substring(last, where - last - Environment.NewLine.Length );
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