namespace Business.Tests.FakeRepositories.Models
{
  using System.Collections.Generic;
  using Data.Models;

  public class FakeCalendar
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public FakeAccess Access { get; set; }
    public FakeUser Owner { get; set; }
    public List<FakeUser> Users { get; set; }
    public List<FakeEvent> Events { get; set; }
  }
}
