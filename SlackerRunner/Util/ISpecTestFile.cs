using System;

namespace SlackerRunner.Util
{
  /// <summary>
  /// Represents a file that will be passed to Xunit test using Theory
  /// </summary>
  public interface ISpecTestFile 
  {
    /// <summary>
    /// The filename of the test
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    /// The actual test name applied by Xunit when run using Theory
    /// </summary>
    string ToString();
  }
}
