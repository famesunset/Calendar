using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DataCore.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataCore.Repository
{
    public class AccessRepo : BaseRepository<Access>, IAccess
    {
        private readonly string connectionString;
        public AccessRepo()
        {
            var builder = new ConfigurationBuilder()   
                .AddJsonFile("launchSettings.json");
            var config = builder.Build();
            connectionString = config["profiles:DataCore:environmentVariables:Server"];
        }
        public IEnumerable<Access> GetNameById(Access @access)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<Access> s = connection.Query<Access>("uspGetAccess", new { @access.Id },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}