using System;
using System.Collections.Generic;
using Business_Layer.Models;

namespace Business_Layer.Services
{
    public interface IEventService
    {
        int AddEvent(string session, int calendarId, Event @event);
        Event GetEvent(string session, int id);
        IEnumerable<Calendar> GetEvents(string session, DateTime beginning, DateUnit dateUnit);
    }
}