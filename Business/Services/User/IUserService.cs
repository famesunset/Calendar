namespace Business.Services.User
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        bool CreateUser(User user);
        User GetUserByIdentityId(string identityId);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        bool AddBrowser(string userId, string browser);
        IEnumerable<Browser> GetBrowsers(int calendarId);
        bool RemoveBrowser(string browser);
    }
}
