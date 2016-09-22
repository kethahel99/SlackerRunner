//
using System;
using Xunit.Abstractions;

namespace SlackerRunner.Util
{

  /// <summary>
  /// Represents a file that will be passed to Xunit test using Theory
  /// </summary>
  public class IndividualSpecTestFile : ISpecTestFile, IXunitSerializable
  {
    // The filename of the test
    public string FileName { get; set; }

    /// <summary>
    /// The actual test name applied by Xunit when run using Theory
    /// </summary>
    public override string ToString()
    {
      return FileName;
    }

    public void Deserialize(IXunitSerializationInfo info)
    {
      FileName = info.GetValue<string>("FileName");
    }

    public void Serialize(IXunitSerializationInfo info)
    {
      info.AddValue("FileName", FileName, typeof(string));
    }
    
  }
}
