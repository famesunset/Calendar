using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Data_Layer;
using Data_Layer.Repository;

namespace Repository
{
    public class AccessRepo : BaseRepository<Access>, IAccess
    {
        public IEnumerable<Data_Layer.Access> GetNameById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Access> s =  connection.Query<Data_Layer.Access>("uspGetAccess", new { id },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}
