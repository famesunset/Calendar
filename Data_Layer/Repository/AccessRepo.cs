using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class AccessRepo : BaseRepository<Access>, IAccess
    {
        public IEnumerable<Data_Layer.Access> GetNameById(Access @access)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Access> s = connection.Query<Data_Layer.Access>("uspGetAccess", new { @access.Id },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}