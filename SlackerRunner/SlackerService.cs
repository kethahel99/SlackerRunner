//
using System;

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
            return profileRunner.Run(testdirectory, profile);
        }
    }
}