namespace Business.Models
{
    using System;

    public class Event : BaseEvent
    {
        public int CalendarId { get; set; }
        public string Description { get; set; }
        public Interval Repeat { get; set; }
        public NotificationSchedule Notify { get; set; }
    }
}