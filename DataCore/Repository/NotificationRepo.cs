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
    public class NotificationRepo : BaseRepository<Notification>, INotification
    {
        private readonly string connectionString;
        public NotificationRepo()
        {
            var builder = new ConfigurationBuilder()   
                .AddJsonFile("launchSettings.json");
            var config = builder.Build();
            connectionString = config["profiles:DataCore:environmentVariables:Server"];
        }
        public IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspCreateNotification",
                    new { eventScheduleId, notificationTime },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}