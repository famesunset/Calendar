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
        public IEnumerable<Event> CreateScheduledEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                DataTable schedule = @event.Schedule.ConvertToDatatable();
                IEnumerable<Event> s = connection.Query<Event>("uspCreateScheduledEvent",
                    new { @event.CalendarId, @event.Notification, @event.Description, @event.Title, schedule, @event.TimeStart, @event.TimeFinish, @event.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Event> CreateInfinityEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspCreateInfinityEvent",
                    new { @event.CalendarId, @event.Notification, @event.Description, @event.Title, @event.RepeatId, @event.TimeStart, @event.TimeFinish, @event.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}