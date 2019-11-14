using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class EventRepo : BaseRepository<Event>, IEvent
    {
        public int CreateScheduledEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable schedule = @event.Schedule.ConvertToDatatable();
                int eventId = connection.ExecuteScalar<int>("uspCreateScheduledEvent",
                    new { @event.CalendarId, @event.Notification, @event.Description, @event.Title, schedule, @event.TimeStart, @event.TimeFinish, @event.AllDay },
                    commandType: CommandType.StoredProcedure);
                return eventId;
            }
        }

        public int CreateInfinityEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int eventId = connection.ExecuteScalar<int>("uspCreateInfinityEvent",
                    new { @event.CalendarId, @event.Notification, @event.Description, @event.Title, @event.RepeatId, @event.TimeStart, @event.TimeFinish, @event.AllDay },
                    commandType: CommandType.StoredProcedure);
                return eventId;
            }
        }

        public void Delete(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspDeleteEvent", new { eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Event> UpdateInfinityEvent(Event @newEvent)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspUpdateInfinityEvent",
                    new { @newEvent.Id, @newEvent.CalendarId, @newEvent.Notification, @newEvent.Description, @newEvent.Title, @newEvent.RepeatId, @newEvent.TimeStart, @newEvent.TimeFinish, @newEvent.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Event> UpdateScheduledEvent(Event @newEvent)
        {
            DataTable schedule = @newEvent.Schedule.ConvertToDatatable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspUpdateScheduledEvent",
                    new { EventId = @newEvent.Id, @newEvent.CalendarId, @newEvent.Notification, @newEvent.Description, @newEvent.Title, Schedule = schedule, @newEvent.TimeStart, @newEvent.TimeFinish, @newEvent.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}