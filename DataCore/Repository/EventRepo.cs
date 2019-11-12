using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataCore.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataCore.Repository
{
    public class EventRepo : BaseRepository<Event>, IEvent
    {
        private readonly string connectionString;
        public EventRepo()
        {
            var builder = new ConfigurationBuilder()   
                .AddJsonFile("launchSettings.json");
            var config = builder.Build();
            connectionString = config["profiles:DataCore:environmentVariables:Server"];
        }
        public IEnumerable<Event> CreateScheduledEvent(Event @event)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<Event> s = connection.Query<Event>("uspCreateInfinityEvent",
                    new { @event.CalendarId, @event.Notification, @event.Description, @event.Title, @event.RepeatId, @event.TimeStart, @event.TimeFinish, @event.AllDay },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public void Delete(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query("uspDeleteEvent", new { eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}