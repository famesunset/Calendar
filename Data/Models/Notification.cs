using System;

namespace Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int EventScheduleId { get; set; }
        public int NotificationMinute { get; set; }

        public Notification(int id, int eventScheduleId, DateTime notificationTime)
        {
            this.Id = id;
            this.EventScheduleId = eventScheduleId;
        }
        public Notification(int eventScheduleId, DateTime notificationTime)
        {
            this.EventScheduleId = eventScheduleId;
        }

        public Notification()
        {
            
        }
    }
}
