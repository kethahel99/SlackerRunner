using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackerRunner.Poco
{
  public class Exception
  {
    public string Class { get; set; }
    public string message { get; set; }
    public List<string> backtrace { get; set; }

  }
}
