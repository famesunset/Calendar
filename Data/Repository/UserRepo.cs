using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class UserRepo : BaseRepository<User>, IUser
    {
        public IEnumerable<User> CreateUser(string name, string mobile, string email, int idIdentity)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<User> s = connection.Query<User>("uspCreateUser", new { name, mobile, email, idIdentity },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}