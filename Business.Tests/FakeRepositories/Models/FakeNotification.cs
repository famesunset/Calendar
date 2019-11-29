namespace Business.Tests.FakeRepositories.Models
{
    using Business.Models;

    public class FakeNotification
    {
        public int EventId { get; set; }
        public int Before { get; set; }
        public NotifyTimeUnit TimeUnit { get; set; }
    }
}