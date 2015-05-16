using System;

namespace SlackerRunner
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly IProcessStartInfoBuilder processStartInfoBuilder;
        private readonly Func<IProcess> processCreator;

        public ProcessRunner(IProcessStartInfoBuilder processStartInfoBuilder, Func<IProcess> processCreator)
        {
            this.processStartInfoBuilder = processStartInfoBuilder;
            this.processCreator = processCreator;
        }

        public ProcessRunner() : this(new ProcessStartInfoBuilder(), () => new ProcessWrapper()) { }

        #region IProcessRunner Members

        public string StandardOutput { get; private set; }
        public string StandardError { get; private set; }

        public void Run(string testDirectory, string batchFileName, string profile, string outputFileName)
        {
            using (var process = processCreator())
            {
                process.StartInfo = processStartInfoBuilder.Build(testDirectory, batchFileName, profile, outputFileName);
                process.Start();
                process.WaitForExit();

                StandardError = process.StandardError.ReadToEnd();
                StandardOutput = process.StandardOutput.ReadToEnd();
            }
        }

        #endregion
    }
}