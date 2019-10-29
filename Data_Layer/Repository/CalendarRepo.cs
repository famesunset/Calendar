using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class CalendarRepo : BaseRepository<Calendar>, ICalendar
    {
        public IEnumerable<Calendar> AddCalendar(string name, int accessId/*? , int userId*/)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Calendar> s = connection.Query<Calendar>("uspCreateCalendar", new { name, accessId },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}
