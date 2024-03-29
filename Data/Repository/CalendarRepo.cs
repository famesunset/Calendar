﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class CalendarRepo : BaseRepository, ICalendar
    {
        public bool CheckDefaultCalendar(int @idCalendar)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                bool checkCalendar = connection.ExecuteScalar<bool>("uspCheckDefaultCalendar", new { @idCalendar },
                    commandType: CommandType.StoredProcedure);
                return checkCalendar;
            }
        }

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

        public bool RemoveCalendar(int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspRemoveCalendar", new { calendarId },
                    commandType: CommandType.StoredProcedure);
                string idCheck = "SELECT Id FROM Calendars WHERE Id = @calendarId";
                return connection.Query<int?>(idCheck, new { calendarId }).SingleOrDefault() == null;
            }
        }

        public void SubscribeUserToCalendar(int userId, int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspSetCalendarToUser", new { calendarId, userId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UnsubscribeUserFromCalendar(int userId, int calendarId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspRemoveCalendarFromUser", new { calendarId, userId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}