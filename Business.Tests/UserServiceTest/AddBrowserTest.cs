using Business.Models;
using Business.Services.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class AddBrowserTest
    {
        private static IUserService userService;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            userService = new UserService(
                new FakeRepositories.FakeUserRepository(),
                new FakeRepositories.FakeCalendarRepository(),
                new FakeRepositories.FakeAllDataRepository()
           );
        }

        [TestMethod]
        public void WrongIdentityIdReturnsFalse()
        {
            var browserId = "aaa-bbb-ccc";

            var result = userService.AddBrowser(null, browserId);
            Assert.AreEqual(result, false);

            result = userService.AddBrowser("noid", browserId);
            Assert.AreEqual(result, false);

           
        }

        [TestMethod]
        public void BrowserIdNullReturnsFalse()
        {
            var validId = "aa-bb-cc-dd";
            var user = new User
            {
                IdentityId = validId,
            };
            userService.CreateUser(user);

            var result = userService.AddBrowser(validId, null);
            Assert.AreEqual(result, false);
        }

    }
}
