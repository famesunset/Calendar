using System;

namespace BusinessCore.Models
{
    public class NotificationSchedule
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}