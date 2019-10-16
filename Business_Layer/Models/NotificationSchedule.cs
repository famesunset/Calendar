using System;

namespace Business_Layer.Models
{
    public class NotificationSchedule
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
    }
}