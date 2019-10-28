using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Data_Layer;
using Data_Layer.Repository.Interfaces;
using Data_Layer.Repository;

namespace Repository
{
    public class EventRepo : BaseRepository<Event>, IEvent
    {
        public IEnumerable<Data_Layer.Event> AddEvent(int calendarId, string notification, string description, string title)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Event> s = connection.Query<Event>("uspCreateEvent",
                    new {calendarId, notification, description, title },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }

        public IEnumerable<Data_Layer.Event> CreateScheduledEvent(int calendarId, string notification, string description, string title,
            DateTime timeStart, DateTime timeFinish)
        {
            using (SqlConnection connection = new SqlConnection(Data_Layer.Properties.Settings.Default.Server))
            {
                IEnumerable<Data_Layer.Event> s = connection.Query<Event>("uspCreateScheduledEvent",
                    new { calendarId, notification, description, title, timeStart, timeFinish },
                    commandType: CommandType.StoredProcedure);
                return s;
            }
        }
    }
}
