using System;
using System.Collections.Generic;
using Business_Layer.Models;

namespace Business_Layer.Services
{
    public interface IEventService
    {
        int AddEvent(string session, int calendarId, Event @event);
        Event GetEvent(string session, int id);
        IEnumerable<Event> GetEvents(string session, ICollection<int> calendarIds, DateTime beginning, DateUnit dateUnit);
        IEnumerable<Event> GetAllEvents(string session, DateTime beginning, DateUnit dateUnit);
    }
}