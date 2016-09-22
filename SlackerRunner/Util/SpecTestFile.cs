//
using System;

namespace SlackerRunner.Util
{

  /// <summary>
  /// Represents a file that will be passed to Xunit test using Theory
  /// </summary>
  public class SpecTestFile : ISpecTestFile
  {
    public string FileName { get; set; }

    /// <summary>
    /// The actual test name applied by Xunit when run using Theory
    /// </summary>
    public override string ToString()
    {
      return FileName;
    }
  }
}
