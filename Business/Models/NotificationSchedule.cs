using System;

namespace Business.Models
{
    public class NotificationSchedule
    {
        public int Id { get; set; }
        public NotifyTimeUnit TimeUnit { get; set; }
        public int Value { get; set; }
    }
}