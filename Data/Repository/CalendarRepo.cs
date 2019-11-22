using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class CalendarRepo : BaseRepository<Calendar>, ICalendar
    {
        public int CreateCalendar(int userId, Calendar calendar)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int calendarId = connection.ExecuteScalar<int>("uspCreateCalendar", new { IdUser = userId, calendar.Name, calendar.AccessId, calendar.ColorId },
                    commandType: CommandType.StoredProcedure);
                return calendarId;
            }
        }

        public IEnumerable<Calendar> GetUserCalendars(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Calendar> calendars = connection.Query<Calendar>("uspGetCalendarsByUserId", new { idUser = userId },
                    commandType: CommandType.StoredProcedure).ToList();
                return calendars;
            }
        }

        public Calendar GetCalendarById(int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Calendar> calendars = connection.Query<Calendar>("uspGetCalendarById", new { calendarId },
                    commandType: CommandType.StoredProcedure);
                return calendars.FirstOrDefault();
            }
        }

        public IEnumerable<User> GetUsersByCalendarId(int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<User> users = connection.Query<User>("uspGetUsersByCalendarId", new { calendarId },
                    commandType: CommandType.StoredProcedure);
                return users;
            }
        }

        public int? RemoveCalendar(int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var removeCalendar = connection.Query("uspRemoveCalendar", new { calendarId },
                    commandType: CommandType.StoredProcedure);
                string idCheck = "SELECT Id FROM Calendars WHERE Id = @calendarId";
                return connection.Query(idCheck, new { calendarId }).SingleOrDefault();
            }
        }
    }
}