using System;
using System.Runtime.Serialization;

namespace SlackerRunner
{
  public class SlackerException : Exception, ISerializable
  {
    /// <summary>
    /// Basic Exception, without specific message
    /// </summary>
    public SlackerException()
    {
    }

    /// <summary>
    /// Exception with description
    /// </summary>
    public SlackerException(String message) : base(message)
    {
    }

    /// <summary>
    /// Exception with description and Inner Exception 
    /// </summary>
    public SlackerException(string message, Exception inner)
        : base(message, inner)
    {
    }

  }
}
