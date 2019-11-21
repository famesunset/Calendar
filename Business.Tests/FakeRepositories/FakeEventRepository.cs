using System.Linq;
using Business.Tests.FakeRepositories.Models;

namespace Business.Tests.FakeRepositories
{
  using System;
  using Data.Models;
  using Data.Repository.Interfaces;
  
  public class FakeEventRepository : IEvent
  {
    private static int eventId;
    public int CreateEvent(Event _event)
    {
      var fakeCalendar = FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(_event.CalendarId));
      if (fakeCalendar != null)
      {
        var fakeEvent = EventToFakeEvent(_event, fakeCalendar);
        FakeRepository.Get.Events.Add(fakeEvent);
        fakeCalendar.Events.Add(fakeEvent);
        return fakeEvent.Id;
      }
      return -1;
    }

    public int CreateInfinityEvent(Event _event)
    {
      var fakeCalendar = FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(_event.CalendarId));
      if (fakeCalendar != null)
      {
        var fakeEvent = EventToFakeInfinityEvent(_event, fakeCalendar);
        FakeRepository.Get.Events.Add(fakeEvent);
        fakeCalendar.Events.Add(fakeEvent);
        return fakeEvent.Id;
      }
      return -1;
    }

    public void UpdateInfinityEvent(Event newEvent)
    {
      var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(newEvent.Id));
      throw new NotImplementedException();
    }

    public void UpdateEvent(Event newEvent)
    {
      throw new NotImplementedException();
    }

    public void Delete(int eventId)
    {
      throw new NotImplementedException();
    }

    private FakeEvent EventToFakeEvent(Event _event, FakeCalendar fakeCalendar)
    {
      return new FakeEvent
      {
        Id = ++eventId,
        Calendar = fakeCalendar,
        Description = _event.Description,
        Start = _event.TimeStart,
        Finish = _event.TimeFinish,
        Title = _event.Title,
        IsAllDay = _event.AllDay,
      };
    }
    
    private FakeInfinityEvent EventToFakeInfinityEvent(Event _event, FakeCalendar fakeCalendar)
    {
      return new FakeInfinityEvent
      {
        Id = ++eventId,
        Calendar = fakeCalendar,
        Description = _event.Description,
        Start = _event.TimeStart,
        Finish = _event.TimeFinish,
        Title = _event.Title,
        IsAllDay = _event.AllDay,
        Repeat = (FakeRepeat) Enum.ToObject(typeof(FakeRepeat), _event.RepeatId)
      };
    }
  }
}