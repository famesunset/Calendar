using Data.Models;

namespace Business.Tests.FakeRepositories.Models
{
  public class FakeInfinityEvent : Event
  {
    public FakeRepeat Repeat { get; set; }
  }
}