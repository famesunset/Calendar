using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Repository.Repository
{
    public class Access : IAccess
    {
        public IEnumerable<Data_Layer.Access> GetNameById(Data_Layer.Access access, int id)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Access> s =  connection.Query<Data_Layer.Access>("uspGetAccess", new { id },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}
