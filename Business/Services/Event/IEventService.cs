namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    public interface IEventService
    {
        int CreateEvent(string loginedUserId, Event @event);
        int CreateScheduledEvent(string loginedUserId, Event @event);
        Event GetEvent(string loginedUserId, int id);
        IEnumerable<Calendar> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit, int[] calendarIds = null);
        void DeleteEvent(string loginedUserId, int eventId);
        void UpdateInfinityEvent(string loginedUserId, Event @newEvent);
        void UpdateScheduledEvent(string loginedUserId, Event @newEvent);
    }
}