using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IUser
    {
        void CreateUser(User user);
        User GetUserByIdentityId(string id);
        User GetUserByEmail(string email);
        IEnumerable<Browser> GetBrowsers(int calendarId);
        void AddBrowser(Browser browser);
        void RemoveBrowser(string browserId);
    }
}