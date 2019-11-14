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

        public void Delete(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                connection.Query("uspDeleteEvent", new { eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Event> UpdateInfinityEvent(Event @oldEvent, Event @newEvent)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspUpdateInfinityEvent",
                    new { @oldEvent.Id, @newEvent.CalendarId, @newEvent.Notification, @newEvent.Description, @newEvent.Title, @newEvent.RepeatId, @newEvent.TimeStart, @newEvent.TimeFinish, @newEvent.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Event> UpdateScheduledEvent(Event @oldEvent, Event @newEvent)
        {
            DataTable schedule = @newEvent.Schedule.ConvertToDatatable();
            using (SqlConnection connection = new SqlConnection(Settings.Default.Server))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspUpdateScheduledEvent",
                    new { @oldEvent.Id, @newEvent.CalendarId, @newEvent.Notification, @newEvent.Description, @newEvent.Title, schedule, @newEvent.TimeStart, @newEvent.TimeFinish, @newEvent.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}