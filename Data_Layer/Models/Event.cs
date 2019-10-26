using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;


namespace Data_Layer
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }

        public Event(int idCalendar, string description, string notification, string title)
        {
            this.CalendarId = idCalendar;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
        }

        public void AddEvent()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AddEvent = connection.Query<Event>("AddEvent",
                    new {this.CalendarId, this.Notification, this.Description, this.Title},
                    commandType: CommandType.StoredProcedure);
            }
        }
    }

    public class AllData
    {
        public int IdCalendar { get; set; }
        public string Name { get; set; }
        public string Access_Name { get; set; }
        public int id_Event { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }
        public int EventSchedule_id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime NotificationTime { get; set; }

        public AllData (int idCalendar, string name, string accessName, int idevent, string description, string notification, string title, int eventScheduleId, DateTime timeStart, DateTime timeFinish, DateTime notificationTime)
        {
            this.IdCalendar = idCalendar;
            this.Name = name;
            this.Access_Name = accessName;
            this.id_Event = idevent;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
            this.EventSchedule_id = eventScheduleId;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
            this.NotificationTime = notificationTime;
        }
        public void GetAllData(int idUser, List<int> idCalendar, DateTime dateTime)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Server))
            {
                var AllData = connection.Query<AllData>("GetAllData", new { idUser, idCalendar, dateTime },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }

}
