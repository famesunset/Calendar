using Data.Repository.Interfaces;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class NotificationRepo : BaseRepository, INotification
    {
        public void UpdateNotification(int eventId, int before, int timeUnitId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspUpdateNotification", new { eventId, before, timeUnitId },
                     commandType: CommandType.StoredProcedure);
            }
        }

        public void CreateNotification(int eventId, int before, int timeUnitId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspCreateNotification", new { eventId, before, timeUnitId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteNotification(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query("uspRemoveNotification", new { eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        //public bool DeleteNotificationSchedule(int eventScheduleId)
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        string sql = "delete from NotificationSchedule where EventScheduleId = " + eventScheduleId;
        //        var c = connection.Query(sql);
        //        string checkId = "select * from NotificationSchedule where EventScheduleId = " + eventScheduleId;
        //        var t = connection.Query(checkId).FirstOrDefault();
        //        if (t == null)
        //        {
        //            return true;
        //        }
        //        else return false;
        //    }
        //}

        //public IEnumerable<Notification> UpdateNotificationInfinity(int eventId, int minutesBefore)
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        IEnumerable<Notification> s = connection.Query<Notification>("uspUpdateNotificationInfinity",
        //            new { eventId, minutesBefore },
        //            commandType: CommandType.StoredProcedure);
        //        return s;
        //    }
        //}

        //public IEnumerable<Notification> UpdateNotificationSchedule(int @eventScheduleId, DateTime @notificationTime)
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        IEnumerable<Notification> s = connection.Query<Notification>("uspUpdateNotificationSchedule",
        //            new { @eventScheduleId, @notificationTime },
        //            commandType: CommandType.StoredProcedure);
        //        return s;
        //    }
        //}
    }
}