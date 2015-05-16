namespace SlackerRunner
{
    public class SlackerResults
    {
        public int Scenarios { get; set; }
        public int Steps { get; set; }
        public int FailedScenarios { get; set; }
        public int PassedScenarios { get; set; }
        public bool Passed { get; set; }
    }
}