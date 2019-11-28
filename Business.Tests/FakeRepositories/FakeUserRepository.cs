namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeUserRepository : IUser
    {
        public void AddBrowser(Browser browser)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Browser> GetBrowsers(int calendarId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserByIdentityId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
