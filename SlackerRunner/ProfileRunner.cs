using System;
using System.Collections.Generic;
using System.IO;

namespace SlackerRunner
{
  public class ProfileRunner
  {
    // public
    public const int DEFAULT_TIMEOUT = 60 * 1000;
    // privates
    private readonly ProcessRunner _processRunner;
    private readonly ResultsParser _resultsParser;


    public ProfileRunner()
        : this(new ProcessRunner(), new ResultsParser(), DEFAULT_TIMEOUT) { }

    public ProfileRunner(ProcessRunner processRunner, ResultsParser resultsParser, int timeoutMilliSeconds)
    {
      _processRunner = processRunner;
      _processRunner.TimeoutMilliseconds = timeoutMilliSeconds;
      _resultsParser = resultsParser;
    }

    /// <summary>
    /// Run a spec test file 
    /// </summary>
    public SlackerResults Run(string testDirectory, string testFile)
    {
      _processRunner.Run(testDirectory, testFile);
      return _resultsParser.Parse(_processRunner.StandardOutput, _processRunner.StandardError);
    }

    /// <summary>
    /// Run a spec test file 
    /// </summary>
    public SlackerResults RunDirectory(string testDirectory, string specDirectory, int timeoutMilliSeconds)
    {
      _processRunner.RunDirectory(testDirectory, specDirectory, timeoutMilliSeconds);
      return _resultsParser.Parse(_processRunner.StandardOutput, _processRunner.StandardError);
    }

    public IEnumerable<SlackerResults> RunDirectoryMultiResults(string testDirectory, string specDirectory, int timeoutMilliSeconds)
    {
      _processRunner.RunDirectory(testDirectory, specDirectory, timeoutMilliSeconds);
      return _resultsParser.ParseJson(_processRunner.StandardOutput, _processRunner.StandardError);
    }

  }
}