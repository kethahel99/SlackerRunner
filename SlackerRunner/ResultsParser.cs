//
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace SlackerRunner
{
    public class ResultsParser : IResultsParser
    {
        // Regexs 
        readonly Regex seconds = new Regex(@"(?<seconds>\d+(.\d+)?)\sseconds", RegexOptions.Compiled);
        readonly Regex FailedSpecs = new Regex(@"(?<failures>\d+)\sfailures", RegexOptions.Compiled);
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
            _res = new SlackerResults
            {
                Info = getLine(result, 1),
                Trace = getLine(result, 2),
                Seconds = FindDouble("seconds", result, seconds),
                FailedSpecs = FindInt("failures", result, FailedSpecs),
                PassedSpecs = FindInt("examples", result, PassedSpecs) - FindInt("failures", result, FailedSpecs),
                Passed = FindInt("failures", result, FailedSpecs) == 0 && string.IsNullOrEmpty(standardError)
            };

            // advertise what what the score is 
            Console.WriteLine( getString() );
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

        /// <summary>
        /// Returns summary of the tests parsed
        /// </summary>
        /// <returns></returns>
        public string getString() 
        {
            return _res.Info + Environment.NewLine +
                _res.Trace + Environment.NewLine + 
                "Passed: " +_res.Passed+ Environment.NewLine + 
                "Passed Specs: " +_res.PassedSpecs + Environment.NewLine + 
                "Failed Specs: " +_res.FailedSpecs + Environment.NewLine + 
                "Seconds: " +_res.Seconds+ Environment.NewLine;
        }
        

    }
}