namespace Business.Tests.FakeRepositories
{
  using System;
  using System.Collections.Generic;
  using Data.Models;
  using Data.Repository.Interfaces;
  
  public class FakeEventRepository : IEvent
  {
    public int CreateScheduledEvent(Event @event)
    {
      throw new NotImplementedException();
    }

    public int CreateInfinityEvent(Event @event)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Event> UpdateInfinityEvent(Event newEvent)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Event> UpdateScheduledEvent(Event newEvent)
    {
      throw new NotImplementedException();
    }

    public void Delete(int eventId)
    {
      throw new NotImplementedException();
    }
  }
}