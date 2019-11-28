namespace Business.Services.User
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        void CreateUser(User user);
        User GetUserByIdentityId(string id);
        User GetUserByEmail(string email);
        void AddBrowser(string userId, string browser);
        IEnumerable<Browser> GetBrowsers(int calendarId);
    }
}
