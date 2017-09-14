using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackerRunner.Poco
{
  public class Example
  {
    public string id { get; set; }
    public string description { get; set; }
    public string full_description { get; set; }
    public string status { get; set; }
    public string file_path { get; set; }
    public int line_number { get; set; }
    public float run_time { get; set; }
    public string pending_message { get; set; }
    public Exception exception { get; set; }
  }
}
