using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace SlackerRunner.IntegrationTests.Util
{

    /// <summary>
    /// Enumerates the spec tests found in /spec directory
    /// </summary>
    class IndividualSpecsTesterResolver : IEnumerable<object[]>
    {
        // List of files
        private static List<IndividualSpecTestFile> _Files = new List<IndividualSpecTestFile>();
        private static string _testDirectory = ""; 

        /// <summary>
        /// Returns the spec tests found in /spec directory
        /// </summary>
        /// <returns></returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            _testDirectory = Path.GetFullPath(SpecsTester.LONG_SPEC_TEST_DIR);
            ProcessDirectory(_testDirectory);
            
            // Enum return
            foreach( object file in _Files )
                yield return new object[] {file};
        }

        /// <summary>
        /// Returns the Enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories  
        /// that are found, and process the files they contain. 

        private static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                // Add relative to the target dir
                var fileUri = new Uri(fileName);
                var referenceUri = new Uri(_testDirectory);
                IndividualSpecTestFile relativeFile = new IndividualSpecTestFile();
                relativeFile.FileName = Uri.UnescapeDataString( referenceUri.MakeRelativeUri(fileUri).ToString() );
                // Absolute path file location
                _Files.Add(relativeFile);
            }

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

    }
}

