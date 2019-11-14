using System;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class NotificationRepo : BaseRepository<Notification>, INotification
    {
        public IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspCreateNotification",
                    new { eventScheduleId, notificationTime },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}