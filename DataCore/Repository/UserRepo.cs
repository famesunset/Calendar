using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataCore.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataCore.Repository
{
    public class UserRepo : BaseRepository<User>, IUser
    {
        private readonly string connectionString;
        public UserRepo()
        {
            var builder = new ConfigurationBuilder()   
                .AddJsonFile("launchSettings.json");
            var config = builder.Build();
            connectionString = config["profiles:DataCore:environmentVariables:Server"];
        }
        public IEnumerable<User> CreateUser(string name, string mobile, string email, int idIdentity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<User> s = connection.Query<User>("uspCreateUser", new { name, mobile, email, idIdentity },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}