using System;

namespace Data.Models
{
    public class AllData
    {
        public int CalendarId { get; set; }
        public string CalendarName { get; set; }
        public string AccessName { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public bool AllDay { get; set; }
        public int RepeatId { get; set; }
        public string CalendarColor { get; set; }
        public int NotificationValue { get; set; }
        public int NotificationTimeUnitId { get; set; }
    }
}
