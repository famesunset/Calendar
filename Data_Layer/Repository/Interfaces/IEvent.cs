using System;
using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface IEvent : IRepository<Event>
    {
        IEnumerable<Event> CreateScheduledEvent(Event @event);
        IEnumerable<Event> CreateInfinityEvent(Event @event);
    }
}