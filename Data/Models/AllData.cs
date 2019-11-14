using System;

namespace Data.Models
{
    public class AllData
    {
        public int IdCalendar { get; set; }
        public string Name { get; set; }
        public string AccessName { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Notification { get; set; }
        public string Title { get; set; }
        public int EventScheduledId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public DateTime NotificationTime { get; set; }
        public bool AllDay { get; set; }

        public AllData(int idCalendar, string name, string accessName, int idevent, string description,
            string notification, string title, int eventScheduleId, DateTime timeStart, DateTime timeFinish,
            DateTime notificationTime)
        {
            this.IdCalendar = idCalendar;
            this.Name = name;
            this.AccessName = accessName;
            this.EventId = idevent;
            this.Description = description;
            this.Notification = notification;
            this.Title = title;
            this.EventScheduledId = eventScheduleId;
            this.TimeStart = timeStart;
            this.TimeFinish = timeFinish;
            this.NotificationTime = notificationTime;
        }

        public AllData()
        {

        }
    }
}
