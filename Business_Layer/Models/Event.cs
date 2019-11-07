using System;
using System.Collections.Generic;

namespace Business_Layer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Color { get; set; }
        // HEX Color
        public DateTime DateStart { get; set; }
        /// <summary>
        /// ????? ??????? ??????????
        /// </summary>
        public DateTime DateFinish { get; set; }
        public DateTime TimeEventStart { get; set; }
        public DateTime TimeEventFinish { get; set; }
        public bool IsAllDay { get; set; }
        public Interval Repeat { get; set; }
        public List<EventSchedule> Schedule { get; set; }
        public NotificationSchedule Notify { get; set; }

        public Event(int id, int calendarId, string title, string description, DateTime dateStart, DateTime dateFinish,
            DateTime timeEventStart, DateTime timeEventFinish,
            bool isAllDay, Interval repeat, List<EventSchedule> schedule, NotificationSchedule notify)
        {
            this.Id = id;
            this.CalendarId = calendarId;
            this.Title = title;
            this.Description = description;
            this.DateStart = dateStart;
            this.DateFinish = dateFinish;
            this.TimeEventStart = timeEventStart;
            this.TimeEventFinish = timeEventFinish;
            this.IsAllDay = isAllDay;
            this.Repeat = repeat;
            this.Schedule = schedule;
            this.Notify = notify;
        }

        public Event()
        {
        }
    }

    public enum Interval
    {
        Day,
        Week,
        Month,
        Year
    }
}