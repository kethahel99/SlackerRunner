using System;
using System.IO;
using Xunit.Abstractions;

namespace SlackerRunner
{
    public class ProfileRunner : IProfileRunner
    {
        private readonly Action<string, string> createOutputFile;
        private readonly Action<string, string, string, string> logResults;
        private readonly IProcessRunner processRunner;
        private readonly Func<string, string, string> readOutputFile;
        private readonly IResultsParser resultsParser;

        public ProfileRunner(IProcessRunner processRunner, IResultsParser resultsParser, Action<string, string> createOutputFile, Func<string, string, string> readOutputFile, Action<string, string, string, string> logResults)
        {
            this.processRunner = processRunner;
            this.resultsParser = resultsParser;
            this.createOutputFile = createOutputFile;
            this.readOutputFile = readOutputFile;
            this.logResults = logResults;
        }

        public ProfileRunner()
            : this(new ProcessRunner(), new ResultsParser(), CreateOutputFile, ReadOutputFile, LogResults) { }

        public SlackerResults Run(string testDirectory, string batchFileName, string profile, string outputFileName)
        {
            //createOutputFile(testDirectory, outputFileName);
            processRunner.Run(testDirectory, batchFileName, profile, outputFileName);
            logResults(testDirectory, outputFileName, processRunner.StandardError, processRunner.StandardOutput);
            //return resultsParser.Parse(readOutputFile(testDirectory, outputFileName), processRunner.StandardError);
            return resultsParser.Parse(processRunner.StandardOutput, processRunner.StandardError);
        }

        private static void CreateOutputFile(string testDirectory, string outputFileName)
        {
            using (File.Create(OutputFileFullName(testDirectory, outputFileName))) { }
        }

        private static void LogResults(string testDirectory, string outputFileName, string standardError, string standardOutput)
        {
            Console.WriteLine("Standard Error: " + standardError);
            Console.WriteLine("Standard Output: " + standardOutput);
            //Console.WriteLine(outputFileName + ":");
            //Console.WriteLine(File.ReadAllText(OutputFileFullName(testDirectory, outputFileName)));
        }

        private static string OutputFileFullName(string testDirectory, string outputFileName)
        {
            return Path.Combine(testDirectory, outputFileName);
        }

        private static string ReadOutputFile(string testDirectory, string outputFileName)
        {
            return File.ReadAllText(OutputFileFullName(testDirectory, outputFileName));
        }
    }
}