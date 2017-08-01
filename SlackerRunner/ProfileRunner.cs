using System;
using System.IO;

namespace SlackerRunner
{
    public class ProfileRunner 
    {
        private readonly ProcessRunner _processRunner;
        private readonly ResultsParser _resultsParser;
        
        public ProfileRunner(ProcessRunner processRunner, ResultsParser resultsParser )
        {
            _processRunner = processRunner;
            _resultsParser = resultsParser;
        }

        public ProfileRunner()
            : this(new ProcessRunner(), new ResultsParser() ) { }
    
    
        public SlackerResults Run(string testDirectory, string testFile)
        {
          _processRunner.Run(testDirectory, testFile);
          return _resultsParser.Parse(_processRunner.StandardOutput, _processRunner.StandardError);
        }
    
    }
}