namespace Data.Models
{
    using System;

    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public bool AllDay { get; set; }
        public int RepeatId { get; set; }
    }
}