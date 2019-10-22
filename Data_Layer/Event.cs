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
    public class Event
    {
        public int id { get; set; }
        public int id_Calendar { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }

        public Event()
        {
        }

        public Event(int idCalendar, string description, string notification, string title)
        {
            this.id_Calendar = idCalendar;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
        }

        public void AddEvent()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AddEvent = connection.Query<Event>("AddEvent",
                    new {this.id_Calendar, this.Notification, this.Description, this.Title},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void GetEventData(int id_User, int id_Calendar, string Title, string Description, DateTime EventTimeStart, DateTime EventTimeFinish, DateTime EventPeriodStart, DateTime EventPeriodFinish)
        {
            
        }

        public void GetAllData(int idUser, int? idCalendar)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AllData = connection.Query<AllData>("GetAllData", new {idUser, id_Calendar},
                    commandType: CommandType.StoredProcedure);
            }
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

        public AllData GetAllData()
        {
            return null;
        }
    }

}
