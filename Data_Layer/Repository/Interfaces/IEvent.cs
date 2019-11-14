using System;
using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface IEvent : IRepository<Event>
    {
        int CreateScheduledEvent(Event @event);
        int CreateInfinityEvent(Event @event);
        IEnumerable<Event> UpdateInfinityEvent(Event @newEvent);
        IEnumerable<Event> UpdateScheduledEvent(Event @newEvent);
    }
}