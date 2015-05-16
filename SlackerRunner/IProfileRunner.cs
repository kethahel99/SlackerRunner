namespace SlackerRunner
{
    public interface IProfileRunner
    {
        SlackerResults Run(string testDirectory, string batchFileName, string profile, string outputFileName);
    }
}