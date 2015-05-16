using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace SlackerRunner.UnitTests
{
    [TestClass]
    public class SlackerServiceTest
    {
        [TestMethod]
        public void RunCallsProfileRunnerAndUsesImpersonationWithUserDetails()
        {
            var user = new User();
            var impersonatorCreator = MockRepository.GenerateStub<Func<User, IDisposable>>();
            var impersonator = MockRepository.GenerateMock<IDisposable>();
            var profileRunner = MockRepository.GenerateMock<IProfileRunner>();

            impersonatorCreator.Expect(x => x.Invoke(user)).Return(impersonator);
            impersonator.Expect(x => x.Dispose());
            profileRunner.Expect(x => x.Run("testDirectory", "batchFileName", "profile", "outputFileName"));

            var SlackerService = new SlackerService(impersonatorCreator, profileRunner);
            SlackerService.Run("testDirectory", "batchFileName", "profile", "outputFileName", user);

            impersonator.VerifyAllExpectations();
            profileRunner.VerifyAllExpectations();
        }

        [TestMethod]
        public void RunCallsProfileRunner()
        {
            var profileRunner = MockRepository.GenerateMock<IProfileRunner>();

            profileRunner.Expect(x => x.Run("testDirectory", "batchFileName", "profile", "outputFileName"));

            var SlackerService = new SlackerService(MockRepository.GenerateStub<Func<User, IDisposable>>(), profileRunner);
            SlackerService.Run("testDirectory", "batchFileName", "profile", "outputFileName");

            profileRunner.VerifyAllExpectations();
        }
    }
}