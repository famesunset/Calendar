namespace BusinessCore.Models
{
    using System.Collections.Generic;

    public class Event : BaseEvent
    {
        public int CalendarId { get; set; }
        public string Description { get; set; }
        public Interval Repeat { get; set; }
        public List<EventSchedule> Schedule { get; set; }
        public NotificationSchedule Notify { get; set; }
    }
}