using System;

namespace Business_Layer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public bool IsAllDay { get; set; }
        public EventSchedule Repeat { get; set; }
        public NotificationSchedule Notify { get; set; }
    }
}