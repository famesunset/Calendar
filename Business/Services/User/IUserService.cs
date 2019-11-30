namespace Business.Services.User
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        bool CreateUser(User user);
        User GetUserByIdentityId(string identityId);
        User GetUserByEmail(string email);
        bool AddBrowser(string userId, string browser);
        IEnumerable<Browser> GetBrowsers(int calendarId);
    }
}
