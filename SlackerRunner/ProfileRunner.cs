using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackerRunner
{
    public class ProfileRunner 
    {
        private readonly Action<string, string> createOutputFile;
        private readonly Action<string, string, string, string> logResults;
        private readonly ProcessRunner processRunner;
        private readonly Func<string, string, string> readOutputFile;
        private readonly ResultsParser resultsParser;
        
  
        public ProfileRunner(ProcessRunner processRunner, ResultsParser resultsParser, Action<string, string> createOutputFile, 
                            Func<string, string, string> readOutputFile, Action<string, string, string, string> logResults)
        {
            this.processRunner = processRunner;
            this.resultsParser = resultsParser;
            this.createOutputFile = createOutputFile;
            this.readOutputFile = readOutputFile;
            this.logResults = logResults;
        }

        public ProfileRunner()
            : this(new ProcessRunner(), new ResultsParser(), CreateOutputFile, ReadOutputFile, LogResults) { }


        public SlackerResults Run(string testDirectory, string testFile)
        {
          processRunner.Run(testDirectory, testFile);
          return resultsParser.Parse(processRunner.StandardOutput, processRunner.StandardError);
        }

        public SlackerResults Run(string testDirectory, string testFile, TestContext testContextInstance)
        {
          processRunner.Run(testDirectory, testFile, testContextInstance);
          return resultsParser.Parse(processRunner.StandardOutput, processRunner.StandardError);
        }
     

        private static void CreateOutputFile(string testDirectory, string outputFileName)
        {
            //using (File.Create(OutputFileFullName(testDirectory, outputFileName))) { }
        }
        private static void LogResults(string testDirectory, string outputFileName, string standardError, string standardOutput)
        {
          Logger.Log("~~~ Standard Error: " + standardError);
          Logger.Log("~~~~ Standard Output: " + standardOutput);
        }
        private static string OutputFileFullName(string testDirectory, string outputFileName)
        {
            return "";
        }
        private static string ReadOutputFile(string testDirectory, string outputFileName)
        {
          return "";
        }


    }
}