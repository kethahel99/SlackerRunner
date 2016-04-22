using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackerRunner
{
  public class SlackerException : Exception
  {
    /// <summary>
    /// Plain Exception
    /// </summary>
    public SlackerException() : base(){}

    /// <summary>
    /// Exception with description
    /// </summary>
    public SlackerException(String message) : base(message){}
  }
}
