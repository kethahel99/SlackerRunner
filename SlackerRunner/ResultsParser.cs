using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackerRunner.Poco;
using System.Globalization;

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
      if (standardError.IndexOf("'slacker' is not recognized") > -1 ||  // not installed yet, or path issue
          standardError.IndexOf("cannot load such file") > -1)  // Ruby configuration bad
      {
        throw new SlackerException("Not able to run slacker, slacker might not be configured correctly.");
      }

      // If no failures found already, use that to communicate the error found
      // in that case it's usually outside of a good run, like bad connection to the database
      if (error && _res.FailedSpecs == 0)
        _res.FailedSpecs++;

      // Get passed and calculate
      _res.PassedSpecs = FindInt("example", result, PassedSpecs) - _res.FailedSpecs;
      _res.Passed = _res.FailedSpecs == 0 && string.IsNullOrEmpty(standardError);

      return _res;
    }


    public IEnumerable<SlackerResults> ParseJson(string result, string standardError)
    {
      // For safety 
      if (standardError == null)
        standardError = "";

      // Find the start and end of the json to parse
      int start = result.IndexOf("{\"version");
      JToken error = null;
      // 0 or more 
      string endMarker = "\"}";
      int end = result.LastIndexOf(endMarker);

      // Parse it 
      List<Example> examples = new List<Example>();
      if (start > -1 && end > -1)
      {
        string json = result.Substring(start, end + endMarker.Length - start);
        // Extract the examples ( test results )
        examples = JObject.Parse(json).SelectToken("examples").ToObject<List<Example>>();
        error = JObject.Parse(json).SelectToken("messages");
      }

      // Check for errors 
      if ( error != null )
      {
        // Create exception from the Json error
        Example ex = InitExample();
        ex.exception.message = error.Last.ToString();
        ex.exception.backtrace.Add(error.First.ToString());
        // add it to the return
        examples.Add(ex);
      }

      Logger.Log("   json, start=" + start + ", end=" + end + ", examples count=" + examples.Count);
      return PocoToResults(examples);
    }

    /// <summary>
    /// Turns POCOs from Json data into SlackerResults
    /// </summary>
    private IEnumerable<SlackerResults> PocoToResults(List<Example> examples)
    {
      List<SlackerResults> ret = new List<SlackerResults>();

      // Loop the examples, turn into SLackerResults
      foreach (Example exs in examples)
      {
        SlackerResults res = new SlackerResults();
        res.Header = string.Empty;
        var duration = TimeSpan.FromSeconds(exs.run_time);
        res.Message = exs.full_description;
        // Take out the prefix, that way it show the 
        // same way as the IspecTestFile implementers
        res.File = exs.file_path.Replace("./", string.Empty) + " - " + exs.full_description + ", duration=" + duration.ToString(@"mm\:ss\.fff");
        // Passed or failed 
        if (exs.status.Equals("passed"))
        {
          res.Passed = true;
          res.PassedSpecs = 1;
        }
        else
        {
          res.Passed = false;
          res.FailedSpecs = 1;
          // Only setting the error - exs.exception.backtrace 
          // holds more granular trace if needed
          res.Trace = exs.full_description + ", Error=" + exs.exception.message;
        }
        // Time 
        res.Seconds = exs.run_time;

        // Add to list 
        ret.Add(res);
      }

      return ret;
    }


    /// <summary>
    /// Returns the given line
    /// </summary>
    private static string getLine(string result, int lineNumber)
    {
      int where = 0;
      int last = 0;
      int count = 0;

      // Trap, no need to process empty string
      if (result == string.Empty)
        return string.Empty;

      // Retrieve the line asked for, cut out line endings 
      while (count != lineNumber)
      {
        last = where;
        where = result.IndexOf(Environment.NewLine, where);
        where += Environment.NewLine.Length;
        count++;
      }

      // Must have encountered error
      if (where - last - Environment.NewLine.Length < 0)
        return string.Empty;
      else
        return result.Substring(last, where - last - Environment.NewLine.Length);
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
      // Make parsing from type Double insensitive to localization settings
      return string.IsNullOrEmpty(match) ? 0 : double.Parse(match, CultureInfo.InvariantCulture);
    }

    private static Example InitExample()
    {
      Example ex = new Example();
      ex.exception = new Poco.Exception();
      ex.exception.message = string.Empty;
      ex.exception.backtrace = new List<string>();
      ex.description = string.Empty; 
      ex.file_path = string.Empty;
      ex.full_description = string.Empty;
      ex.id = string.Empty;
      ex.line_number = 0;
      ex.pending_message = string.Empty;
      ex.run_time = 0;
      ex.status = string.Empty;
      return ex;
    }


  }
}