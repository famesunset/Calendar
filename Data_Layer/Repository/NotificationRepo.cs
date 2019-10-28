using System;
using Data_Layer.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Data_Layer.Repository;
using Data_Layer;

namespace Repository
{
    public class NotificationRepo : BaseRepository<Notification>, INotification
    {
        public IEnumerable<Data_Layer.Notification> CreateNotification(int eventScheduleId, DateTime notificationTime)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Notification> s = connection.Query<Notification>("uspCreateNotification",
                    new { eventScheduleId, notificationTime },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}