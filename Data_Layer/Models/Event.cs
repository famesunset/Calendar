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
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime TimePeriodStart { get; set; }
        public DateTime TimePeriodFinish { get; set; }

        public Event(int id, int calendarId, string description, string notification, string title, 
            DateTime timeStart, DateTime timeFinish)
        {
            this.Id = id;
            this.CalendarId = calendarId;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
        }

        public Event(int id, int calendarId, string description, string notification, string title)
        {
            this.Id = id;
            this.CalendarId = calendarId;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
        }
    }
}
