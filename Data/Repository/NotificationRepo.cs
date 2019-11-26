using System;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data.Models;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class NotificationRepo : BaseRepository<Notification>, INotification
    {
        public IEnumerable<Notification> UpdateNotification(int eventId, int minutesBefore)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspUpdateNotification",
                    new { eventId, minutesBefore },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Notification> UpdateNotificationInfinity(int eventId, int minutesBefore)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspUpdateNotificationInfinity",
                    new { eventId, minutesBefore },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Notification> UpdateNotificationSchedule(int @eventScheduleId, DateTime @notificationTime)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<Notification> s = connection.Query<Notification>("uspUpdateNotificationSchedule",
                    new { @eventScheduleId, @notificationTime },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

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

        public bool DeleteNotificationInfinity(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sql = "delete from NotificationInfinity where EventId = " + eventId;
                var c = connection.Query(sql);
                string checkId = "select * from NotificationInfinity where EventId = " + eventId;
                var t = connection.Query(checkId).FirstOrDefault();
                if (t == null)
                {
                    return true;
                }
                else return false;
            }
        }

        public bool DeleteNotificationSchedule(int eventScheduleId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sql = "delete from NotificationSchedule where EventScheduleId = " + eventScheduleId;
                var c = connection.Query(sql);
                string checkId = "select * from NotificationSchedule where EventScheduleId = " + eventScheduleId;
                var t = connection.Query(checkId).FirstOrDefault();
                if (t == null)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}