using Dapper;
using System.Collections.Generic;
using System.Data;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class AccessRepo : BaseRepository, IAccess
    {
        public IEnumerable<Access> GetNameById(Access @access)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Access> s = connection.Query<Access>("uspGetAccess", new { @access.Id },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}