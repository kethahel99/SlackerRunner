//
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SlackerRunner
{
  public class SlackerService
  {
    private readonly Func<User, IDisposable> _impersonatorCreator;
    private readonly ProfileRunner _profileRunner = new ProfileRunner();
    private int _timeout = 0;

    public SlackerService() : this((x) => new Impersonator(x.Name, x.Domain, x.Password), ProfileRunner.DEFAULT_TIMEOUT) { }

    public SlackerService(int timeoutMilliSeconds) : this((x) => new Impersonator(x.Name, x.Domain, x.Password), timeoutMilliSeconds) { }

    public SlackerService(Func<User, IDisposable> impersonatorCreator, int timeoutMilliseconds)
    {
      _timeout = timeoutMilliseconds;
      _impersonatorCreator = impersonatorCreator;
    }


    public int TimeoutMilliseconds
    {
      set { _timeout = value; }
      get { return _timeout; }
    }

    /// <summary>
    /// Runs the test in the given testfile.
    /// </summary>
    /// <param name="testdirectory">Base directory where database.yml is located. </param>
    /// <param name="specfile">The spec test file to run.</param>
    /// <param name="user">Imprisonate user.</param>
    /// <returns></returns>
    public SlackerResults Run(string testdirectory, string specfile, User user)
    {
      using (_impersonatorCreator(user))
      {
        return _profileRunner.Run(testdirectory, specfile);
      }
    }

    /// <summary>
    /// Runs the test in the given testfile.
    /// </summary>
    /// <param name="testdirectory">Base directory where database.yml is located. </param>
    /// <param name="specfile">The spec test file to run.</param>
    /// <returns></returns>
    public SlackerResults Run(string testdirectory, string specfile)
    {
      // Make sure directory and file exists before heading further
      if (!Directory.Exists(testdirectory))
        throw new SlackerException("The directory does not exist, directory=" + testdirectory);

      // Only test for specific test file when wildcars are not in use
      if (specfile.IndexOf('*') == -1 && !File.Exists(specfile))
        throw new SlackerException("The file does not exist, file=" + specfile);

      // Go for it
      return _profileRunner.Run(testdirectory, specfile);
    }
    
    /// <summary>
    /// Runs the tests in the given directory and sub directories.
    /// </summary>
    /// <param name="testdirectory">Base directory where database.yml is located. </param>
    /// <param name="specDirectory">The specs test directory.</param>
    public SlackerResults RunDirectory(string testDirectory, string specDirectory, int timeoutMilliseconds)
    {
      // Make sure directory and file exists before heading further
      if (!Directory.Exists(testDirectory))
        throw new SlackerException("The directory does not exist, directory=" + testDirectory);

      // Go for it
      return _profileRunner.RunDirectory(testDirectory, specDirectory, timeoutMilliseconds);
    }


    /// <summary>
    /// Runs the tests in the given directory and sub directories, returns SlackerResults for each test.
    /// </summary>
    /// <param name="testdirectory">Base directory where database.yml is located. </param>
    /// <param name="specDirectory">The specs test directory.</param>
    public IEnumerable<SlackerResults> RunDirectoryMultiResults(string testDirectory, string specDirectory, int timeoutMilliseconds)
    {
      // Make sure directory and file exists before heading further
      if (!Directory.Exists(testDirectory))
        throw new SlackerException("The directory does not exist, directory=" + testDirectory);

      // Go for it
      return _profileRunner.RunDirectoryMultiResults(testDirectory, specDirectory, timeoutMilliseconds);
    }

  }
}