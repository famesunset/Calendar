namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    public interface IEventService
    {
        int CreateEvent(string loginedUserId, Event @event, int timeOffset);
        Event GetEvent(string loginedUserId, int id);
        IEnumerable<BaseEvent> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit, int[] calendarIds, int timeOffset);
        void DeleteEvent(string loginedUserId, int eventId);
        void UpdateEvent(string loginedUserId, Event @newEvent);
    }
}