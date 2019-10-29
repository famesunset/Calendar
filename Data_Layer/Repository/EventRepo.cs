using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Properties;
using Data_Layer.Repository.Interfaces;

namespace Data_Layer.Repository
{
    public class EventRepo : BaseRepository<Event>, IEvent
    {
        public IEnumerable<Event> AddEvent(int calendarId, string notification, string description, string title)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspCreateEvent",
                    new {calendarId, notification, description, title}, 
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Event> CreateScheduledEvent(int calendarId, string notification, string description,
            string title,
            DateTime timeStart, DateTime timeFinish)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspCreateScheduledEvent",
                    new {calendarId, notification, description, title, timeStart, timeFinish},
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}