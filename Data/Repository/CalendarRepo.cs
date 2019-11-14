using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class CalendarRepo : BaseRepository<Calendar>, ICalendar
    {
        public IEnumerable<Calendar> AddCalendar(User user, Calendar calendar)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Calendar> s = connection.Query<Calendar>("uspCreateCalendar", new { user.IdUser, calendar.Name, calendar.AccessId },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Calendar> GetUserCalendars(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Calendar> s = connection.Query<Calendar>("uspGetCalendarsByUserId", new { idUser = userId },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}