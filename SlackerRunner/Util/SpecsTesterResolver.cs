using System;
using System.Collections.Generic;
using System.IO;


namespace SlackerRunner.Util
{

    /// <summary>
    /// Enumerates the spec tests found in /spec directory
    /// </summary>
    public class SpecsTesterResolver 
    {
        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories  
        /// that are found, and process the files they contain. 
        /// </summary>
        public static List<ISpecTestFile> ProcessDirectory(string targetDirectory, Type type )
        {
            return ProcessDirectory(targetDirectory, targetDirectory, type );
        }

        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories  
        /// that are found, and process the files they contain. 
        /// </summary>
        public static List<ISpecTestFile> ProcessDirectory(string targetDirectory, string startDirectory, Type type )
        {
            //Logger.Log("~~~ directories, target=" + targetDirectory + ", startdirectory=" + startDirectory);
            List <ISpecTestFile> testFiles = new List<ISpecTestFile>();
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                // Add relative to the target dir
                var fileUri = new Uri(fileName);
                var referenceUri = new Uri(startDirectory);
                ISpecTestFile relativeFile = (ISpecTestFile)Activator.CreateInstance(type);
                //ISpecTestFile relativeFile = new IndividualSpecTestFile();
                relativeFile.FileName = Uri.UnescapeDataString( referenceUri.MakeRelativeUri(fileUri).ToString() );
                // Absolute path file location
                testFiles.Add(relativeFile);
            }

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                testFiles.AddRange( ProcessDirectory(subdirectory, startDirectory, type) );

            // Back to caller 
            return testFiles;
        }

    }
}

