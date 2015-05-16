using System.Diagnostics;

namespace SlackerRunner
{
    public interface IProcessStartInfoBuilder
    {
        ProcessStartInfo Build(string testDirectory, string batchFileName, string outputFileName, string profile);
    }
}