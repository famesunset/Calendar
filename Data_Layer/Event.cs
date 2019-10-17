using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Interop;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Dapper;
using System.ComponentModel;


namespace Data_Layer
{
    class Event
    {
        /// <summary>
        /// Get data from view to put it into the db.
        /// </summary>
        /// <param name="id_User">id of User</param>
        /// <param name="id_Calendar">id of Calendar</param>
        /// <param name="Title">Title of the Event</param>>
        /// <param name="Description">Description of the Event</param>
        /// <param name="EventTimeStart">The event starts at this time (day, hours and minutes)</param>
        /// <param name="EventTimeFinish">The Event starts at this time (day, hours and minutes)</param>
        /// <param name="EventPeriodStart">Datetime of the event when it is started (day period since)</param>
        /// <param name="EventPeriodFinish">Datetime of the event when it is finished (day period till)</param>
        public void GetEventData(int id_User, int id_Calendar, string Title, string Description, DateTime EventTimeStart, DateTime EventTimeFinish, DateTime EventPeriodStart, DateTime EventPeriodFinish)
        {
            
        }

        public void GetAllData(int idUser, int? idCalendar)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "WIN-BR9AAF20AAG",
                UserID = "sa",
                Password = "Sunsetfame05!",
                InitialCatalog = "Calendar"
            };

            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            List<int> idCalendars = new List<int>();
            idCalendars.Add(4);
            idCalendars.Add(6);
            idCalendars.ConvertToDatatable();
            
            var AllData = connection.Query<AllData>("GetAllData", new { idUser, idCalendars},
                    commandType: CommandType.StoredProcedure);
        }
    }

    public class AllData
    {
        public int IdCalendar { get; set; }
        public string CalendarName { get; set; }
        public string Access { get; set; }
        public int IdEvent { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }
        public int IdEventSchedule { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime NotificationTime { get; set; }
    }

}
