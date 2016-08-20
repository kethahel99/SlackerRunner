using System;
using System.IO;

namespace SlackerRunner
{
    public class ProfileRunner 
    {
        private readonly ProcessRunner processRunner;
        private readonly ResultsParser resultsParser;
        
        public ProfileRunner(ProcessRunner processRunner, ResultsParser resultsParser )
        {
            this.processRunner = processRunner;
            this.resultsParser = resultsParser;
        }

        public ProfileRunner()
            : this(new ProcessRunner(), new ResultsParser() ) { }
    
    
        public SlackerResults Run(string testDirectory, string testFile)
        {
          processRunner.Run(testDirectory, testFile);
          return resultsParser.Parse(processRunner.StandardOutput, processRunner.StandardError);
        }
    
    }
}