namespace Business.Models
{
    public class Event : BaseEvent
    {
        public int CalendarId { get; set; }
        public string Description { get; set; }
        public Interval Repeat { get; set; }
        public NotificationSchedule Notify { get; set; }
        public User Creator { get; set; }

        public Event()
        {
            Notify = new NotificationSchedule();
        }
    }
}