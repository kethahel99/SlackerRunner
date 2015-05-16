namespace SlackerRunner
{
    public interface IResultsParser
    {
        SlackerResults Parse(string result, string standardError);
    }
}