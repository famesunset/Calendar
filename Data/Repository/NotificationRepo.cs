using System;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class NotificationRepo : BaseRepository<NotificationSchedule>, INotification
    {
        public IEnumerable<Notification> CreateNotification(Notification notification)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspCreateNotification",
                    new { notification.EventId, notification.MinutesBefore },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<NotificationSchedule> CreateScheduleNotification(int eventScheduleId, DateTime notificationTime)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<NotificationSchedule> s = connection.Query<NotificationSchedule>("uspCreateScheduleNotification",
                    new { eventScheduleId, notificationTime },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}