using System;

namespace Business_Layer.Models
{
    public class NotificationSchedule
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}