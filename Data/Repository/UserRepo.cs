using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class UserRepo : BaseRepository, IUser
    {
        public void AddBrowser(Browser browser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspSetBrowserToUser", new { userId = browser.UserId, browser = browser.BrowserId },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public void RemoveBrowser(string browserId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspRemoveBrowser", new { browser = browserId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspCreateUser", new { name = user.Name, mobile = user.Mobile, email = user.Email, identityId = user.IdIdentity, picture = user.Picture },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Browser> GetBrowsers(int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var browsers = connection.Query<Browser>("uspGetBrowsersByCalendar", new { calendarId },
                    commandType: CommandType.StoredProcedure);
                return browsers;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                User user = connection.Query<User>("uspGetUserByEmail", new { email },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
                return user;
            }
        }

        public User GetUserByIdentityId(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                User user = connection.Query<User>("uspGetUserByIdentityId", new { identityId = id },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
                return user;
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                User user = connection.Query<User>("uspGetUserById", new { id },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
                return user;
            }
        }
    }
}