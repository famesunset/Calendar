using System;

namespace Business.Tests.FakeRepositories.Models
{
  public class FakeEvent
  {
    public int Id { get; set; }
    public FakeCalendar Calendar { get; set; }
    public string Description { get; set; }
    public FakeNotification Notification { get; set; }
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public bool IsAllDay { get; set; }
  }

  public class FakeNotification
  {
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
  }
}