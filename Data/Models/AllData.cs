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

        public AllData(int calendarId, string calendarName, string accessName, int eventId, string description,
            string title, int eventScheduleId, DateTime timeStart, DateTime timeFinish)
        {
            this.CalendarId = calendarId;
            this.CalendarName = calendarName;
            this.AccessName = accessName;
            this.EventId = eventId;
            this.Description = description;
            this.Title = title;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
        }

        public AllData()
        {

        }
    }
}
