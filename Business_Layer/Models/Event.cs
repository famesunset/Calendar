using System;

namespace Business_Layer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Color { get; set; }
        // HEX Color
        public bool IsAllDay { get; set; }
        public NotificationSchedule Notify { get; set; }
    }
}