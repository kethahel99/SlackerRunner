namespace SlackerRunner
{
    public interface IProcessRunner
    {
        string StandardOutput { get; }
        string StandardError { get; }
        void Run(string testDirectory, string batchFileName, string profile, string outputFileName);
    }
}