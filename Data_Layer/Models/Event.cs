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
