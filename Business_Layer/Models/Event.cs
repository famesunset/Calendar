using System;

namespace Business_Layer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { set; get; }
        public int CalendarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public EventSchedule Repeat { get; set; }
        public NotificationSchedule Notification { get; set; }
    }
}