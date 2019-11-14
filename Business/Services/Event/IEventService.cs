namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    public interface IEventService
    {
        int AddEvent(string session, Event @event);
        int CreateScheduledEvent(string session, Event @event);
        Event GetEvent(string session, int id);
        IEnumerable<Calendar> GetEvents(string session, DateTime beginning, DateUnit dateUnit);
        void DeleteEvent(string session, int eventId);
        void UpdateInfinityEvent(Event @newEvent);
        void UpdateScheduledEvent(Event @newEvent);
    }
}