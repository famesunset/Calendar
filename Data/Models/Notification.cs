﻿using System;

namespace Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int EventScheduleId { get; set; }
        public DateTime NotificationTime { get; set; }

        public Notification(int id, int eventScheduleId, DateTime notificationTime)
        {
            this.Id = id;
            this.EventScheduleId = eventScheduleId;
            this.NotificationTime = notificationTime;
        }
        public Notification(int eventScheduleId, DateTime notificationTime)
        {
            this.EventScheduleId = eventScheduleId;
            this.NotificationTime = notificationTime;
        }

        public Notification()
        {
            
        }
    }
}
