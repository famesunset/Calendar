using System;
using System.Collections.Generic;

namespace Data.Models
{
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

        public Event(int calendarId, string description, string notification, string title, List<EventSchedule> eventSchedule,
            DateTime timeStart, DateTime timeFinish, bool allDay)
        {
            this.CalendarId = calendarId;
            this.Description = description;
            this.Title = title;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
            this.AllDay = allDay;
        }

        public Event(int calendarId, string description, string notification, string title, int repeatId,
            DateTime timeStart, DateTime timeFinish, bool allDay)
        {
            this.CalendarId = calendarId;
            this.Description = description;
            this.Title = title;
            this.RepeatId = repeatId;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
            this.AllDay = allDay;
        }

        public Event(int id, int calendarId, string description, string notification, string title, DateTime timeStart, DateTime timeFinish)
        {
            this.Id = id;
            this.CalendarId = calendarId;
            this.Description = description;
            this.Title = title;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
        }

        public Event()
        {

        }
    }
}