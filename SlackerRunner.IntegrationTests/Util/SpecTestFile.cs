//
using System;

namespace SlackerRunner.IntegrationTests.Util
{

    /// <summary>
    /// Represents a file that will be passed to Xunit test using Theory
    /// </summary>
    public class SpecTestFile
    {
        // The filename of the test
        public string FileName = "";
        
        /// <summary>
        /// The actual test name applied by Xunit when run using Theory
        /// </summary>
        public override string ToString()
        {
            return FileName;
        }
    }
}
