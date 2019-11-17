using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class UserRepo : BaseRepository<User>, IUser
    {
        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspCreateUser", new { name = user.Name, mobile = user.Mobile, email = user.Email, identityId = user.IdIdentity, picture = user.Picture },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public User GetUserByIdentityId(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<User> s = connection.Query<User>("uspGetUserByIdentityId", new { identityId = id },
                    commandType: CommandType.StoredProcedure);
                return s.SingleOrDefault();
            }
        }
    }
}