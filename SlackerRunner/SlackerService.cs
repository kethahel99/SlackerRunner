//
using System;

namespace SlackerRunner
{
    public class SlackerService
    {
        private readonly Func<User, IDisposable> impersonatorCreator;
        private readonly IProfileRunner profileRunner;

        public SlackerService() : this((x) => new Impersonator(x.Name, x.Domain, x.Password), new ProfileRunner()) { }

        public SlackerService(Func<User, IDisposable> impersonatorCreator, IProfileRunner profileRunner)
        {
            this.impersonatorCreator = impersonatorCreator;
            this.profileRunner = profileRunner;
        }

        public SlackerResults Run(string testdirectory, string batchfilename, string profile, string outputfilename, User user)
        {
            using (impersonatorCreator(user))
            {
                return profileRunner.Run(testdirectory, batchfilename, profile, outputfilename);
            }
        }

        public SlackerResults Run(string testdirectory, string batchfilename, string profile, string outputfilename)
        {
            return profileRunner.Run(testdirectory, batchfilename, profile, outputfilename);
        }
    }
}