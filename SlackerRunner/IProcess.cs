using System;
using System.Diagnostics;
using System.IO;

namespace SlackerRunner
{
    public interface IProcess : IDisposable
    {
        ProcessStartInfo StartInfo { get; set; }
        StreamReader StandardError { get; }
        StreamReader StandardOutput { get; }
        bool Start();
        void WaitForExit();
    }
}