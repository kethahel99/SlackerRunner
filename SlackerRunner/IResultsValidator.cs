namespace SlackerRunner
{
    public interface IResultsValidator
    {
        bool IsValid { get; set; }
        void Validate(string standarderror, string output);
    }
}