using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Data.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class EventRepo : BaseRepository, IEvent
    {
        public int CreateEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int eventId = connection.ExecuteScalar<int>("uspCreateEvent",
                    new { @event.CalendarId, @event.Description, @event.Title, @event.RepeatId, @event.TimeStart, @event.TimeFinish, @event.AllDay, @event.CreatorId },
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

        public void UpdateEvent(Event newEvent)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query<Event>("uspUpdateEvent",
                    new { newEvent.Id, newEvent.CalendarId, newEvent.Description, newEvent.Title, newEvent.RepeatId, newEvent.TimeStart, newEvent.TimeFinish, newEvent.AllDay },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}