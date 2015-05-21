namespace SlackerRunner
{
    public class SlackerResults
    {
        public string What { get; set; }
        public double Seconds { get; set; }
        public int FailedSpecs { get; set; }
        public int PassedSpecs { get; set; }
        public bool Passed { get; set; }
    }
}