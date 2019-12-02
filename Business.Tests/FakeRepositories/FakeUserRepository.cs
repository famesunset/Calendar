namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business.Tests.FakeRepositories.Models;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeUserRepository : IUser
    {
        private static int fakeBrowserId;
        private static int fakeUserId;

        public void AddBrowser(Browser browser)
        {
            var user = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(browser.UserId));
            if (user != null)
            {
                user.Browsers.Add(new FakeBrowser { Id = fakeBrowserId++, BrowserId = browser.BrowserId, User = user });
            }
        }

        public void RemoveBrowser(string browserId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            user.IdUser = ++fakeUserId;
            var fUser = FakeConverters.UserToFakeUser(user);
            FakeRepository.Get.Users.Add(fUser);
            var cRepos = new FakeCalendarRepository();
            var calendarId = cRepos.CreateCalendar(fUser.Id, new Calendar
            {
                AccessId = (int)Business.Models.Access.Private,
                Name = "Default",
                UserOwnerId = fUser.Id,
                ColorId = ColorFake.Default.Id,
                ColorHex = ColorFake.Default.Hex,
            });
            var calendar = FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(calendarId));
            fUser.DefaultCalendar = calendar;
        }

        public IEnumerable<Browser> GetBrowsers(int calendarId)
        {
            var calendar = FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(calendarId));
            return calendar.Users.SelectMany(u => u.Browsers.Select(b => FakeConverters.FakeBrowserToBrowser(b)));
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            var user = FakeRepository.Get.Users.SingleOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return FakeConverters.FakeUserToUser(user);
        }

        public User GetUserByIdentityId(string id)
        {
            var user = FakeRepository.Get.Users.SingleOrDefault(u => u.IdentityId.Equals(id, StringComparison.OrdinalIgnoreCase));
            return FakeConverters.FakeUserToUser(user);
        }
    }
}
