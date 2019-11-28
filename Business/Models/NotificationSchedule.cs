namespace Business.Models
{
    public class NotificationSchedule
    {
        public int EventId { get; set; }
        public NotifyTimeUnit TimeUnit { get; set; }
        public int Value { get; set; }
    }
}