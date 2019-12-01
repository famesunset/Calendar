namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    public interface IEventService
    {
        int CreateEvent(string loginedUserId, Event @event, int timeOffset);
        Event GetEvent(string loginedUserId, int eventId, int timeOffset);
        string GetEventLink(string loginedUserId, int eventId, string domain, int timeOffset);
        IEnumerable<BaseEvent> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit, int[] calendarIds, int timeOffset);
        bool DeleteEvent(string loginedUserId, int eventId);
        bool UpdateEvent(string loginedUserId, Event @newEvent);
    }
}