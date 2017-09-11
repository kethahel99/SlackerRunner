using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SlackerRunner
{
  public class ResultsParser 
  {
    // Regexs 
    const string _FAILURE = "failure";
    readonly Regex seconds = new Regex(@"(?<seconds>\d+(.\d+)?)\sseconds", RegexOptions.Compiled);
    readonly Regex FailedSpecs = new Regex(@"(?<failure>\d+)\sfailure", RegexOptions.Compiled);
    readonly Regex PassedSpecs = new Regex(@"(?<example>\d+)\sexample", RegexOptions.Compiled);
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
      // For safety 
      if (standardError == null)
        standardError = "";

      // Extract results 
      _res = new SlackerResults();
      // Fill it 
      _res.Header = getLine(result, 1);
      _res.Message = result;
      _res.Trace = getLine(result, 2);
      _res.Seconds = FindDouble("seconds", result, seconds);
      _res.FailedSpecs = FindInt(_FAILURE, result, FailedSpecs);

      // Check for error in result and standardError
      bool error = Regex.IsMatch(result + standardError, "error", RegexOptions.IgnoreCase);
            
      // Trap slacker not found 
      if(standardError.IndexOf("'slacker' is not recognized") >-1 ||  // not installed yet, or path issue
          standardError.IndexOf("cannot load such file") > -1 )  // Ruby configuration bad
        throw new SlackerException("Not able to run slacker, slacker might not be configured correctly.");

      // If no failures found already, use that to communicate the error found
      // in that case it's usually outside of a good run, like bad connection to the database
      if (error && _res.FailedSpecs == 0 )
        _res.FailedSpecs++;
            
      // Get passed and calculate
      _res.PassedSpecs = FindInt("example", result, PassedSpecs) - _res.FailedSpecs;
      _res.Passed = _res.FailedSpecs == 0 && string.IsNullOrEmpty(standardError);

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
            
      // Trap, no need to process empty string
      if (result == string.Empty)
        return string.Empty;

      // Retrieve the line asked for, cut out line endings 
      while ( count != lineNumber)
      {
        last = where;
        where = result.IndexOf(Environment.NewLine, where  );
        where += Environment.NewLine.Length;
        count++;
      }
            
      // Must have encountered error
      if (where - last - Environment.NewLine.Length < 0)
        return string.Empty;
      else
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