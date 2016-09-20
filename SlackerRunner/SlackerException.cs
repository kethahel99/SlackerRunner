using System;


namespace SlackerRunner
{
  public class SlackerException : Exception
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

  }
}
