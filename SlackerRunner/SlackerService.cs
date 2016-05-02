//
using System;
using System.IO;

namespace SlackerRunner
{
    public class SlackerService
    {
        private readonly Func<User, IDisposable> impersonatorCreator;
        private readonly ProfileRunner profileRunner;

        public SlackerService() : this((x) => new Impersonator(x.Name, x.Domain, x.Password), new ProfileRunner()) { }
        
        public SlackerService(Func<User, IDisposable> impersonatorCreator, ProfileRunner profileRunner)
        {
            this.impersonatorCreator = impersonatorCreator;
            this.profileRunner = profileRunner;
        }
        
        public SlackerResults Run(string testdirectory, string profile, User user)
        {
            using (impersonatorCreator(user))
            {
                return profileRunner.Run(testdirectory, profile );
            }
        }

        public SlackerResults Run(string testdirectory, string profile )
        {
            // Make sure directory and file exist before heading further
            if (!Directory.Exists(testdirectory))
              throw new SlackerException("The directory does not exist, directory=" + testdirectory);      
            if (!File.Exists(profile))
              throw new SlackerException("The file does not exist, file=" + profile);

            return profileRunner.Run(testdirectory, profile);
        }


    }
}