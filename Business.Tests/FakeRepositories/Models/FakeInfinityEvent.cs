using Data.Models;

namespace Business.Tests.FakeRepositories.Models
{
  public class FakeInfinityEvent : FakeEvent
  {
    public FakeRepeat Repeat { get; set; }
  }
}