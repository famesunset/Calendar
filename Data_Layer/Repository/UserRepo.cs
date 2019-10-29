using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class UserRepo : BaseRepository<User>, IUser
    {
        public IEnumerable<Data_Layer.User> CreateUser(string name, string mobile, string email, int idIdentity)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.User> s = connection.Query<User>("uspCreateUser", new { name, mobile, email, idIdentity },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}