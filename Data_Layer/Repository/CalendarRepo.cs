using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class CalendarRepo : BaseRepository<Calendar>, ICalendar
    {
        /// <summary>
        /// </summary>
        /// <param name="userId">id user</param>
        /// <param name="name">name of calendar</param>
        /// <param name="accessId">1 = private</param>
        /// <returns></returns>
        public IEnumerable<Calendar> AddCalendar(User @user, Calendar @calendar)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Calendar> s = connection.Query<Calendar>("uspCreateCalendar", new { @user.IdUser, @calendar.Name, @calendar.AccessId },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Calendar> GetUserCalendars(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Calendar> s = connection.Query<Calendar>("uspGetCalendarsByUserId", new { idUser = userId },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}