using System;

namespace Data.Models
{
    public class NotificationSchedule
    {
        public int Id { get; set; }
        public int EventScheduleId { get; set; }
        public int NotificationMinute { get; set; }

        public NotificationSchedule(int id, int eventScheduleId, DateTime notificationTime)
        {
            this.Id = id;
            this.EventScheduleId = eventScheduleId;
        }
        public NotificationSchedule(int eventScheduleId, DateTime notificationTime)
        {
            this.EventScheduleId = eventScheduleId;
        }

        public NotificationSchedule()
        {
            
        }
    }
}
