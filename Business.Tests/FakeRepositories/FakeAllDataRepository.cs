namespace Business.Tests.FakeRepositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Data.Models;
  using Data.Repository.Interfaces;
  
  public class FakeAllDataRepository : IAllData
  {
    public IEnumerable<AllData> GetDataEvents(int userId, IEnumerable<Calendar> calendarsList, DateTime dateTimeStart, DateTime dateTimeFinish)
    {
      var calendars = FakeRepository.Get.Calendars
        .Where(
          c => calendarsList.Any(cl => cl.Id.Equals(c.Id)) && 
          c.Users.Any(u => u.Id.Equals(userId))
        );

      var events = calendars.SelectMany(c => c.Events)
        .Where(e => e.Start >= dateTimeStart && e.Finish <= dateTimeFinish);

      var allDataEvents = events.Select(e => new AllData
      {
        EventId = e.Id, 
        IdCalendar = e.Calendar.Id,
        Description = e.Description,
        Title = e.Title,
        Notification = e.Notification.Text,
        AccessName = e.Calendar.Access.ToString(),
        AllDay = e.IsAllDay,
        NotificationTime = e.Notification.Time,
        TimeStart = e.Start,
        TimeFinish = e.Finish,
        Name = e.Calendar.Name,
        EventScheduledId = 0,
      });
      
      return allDataEvents;
    }

    public AllData GetEvent(int eventId)
    {
      var _event = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
      if (_event != null)
      {
        return new AllData
        {
          EventId = _event.Id, 
          IdCalendar = _event.Calendar.Id,
          Description = _event.Description,
          Title = _event.Title,
          Notification = _event.Notification.Text,
          AccessName = _event.Calendar.Access.ToString(),
          AllDay = _event.IsAllDay,
          NotificationTime = _event.Notification.Time,
          TimeStart = _event.Start,
          TimeFinish = _event.Finish,
          Name = _event.Calendar.Name,
          EventScheduledId = 0,
        }; 
      }

      return null;
    }
  }
}