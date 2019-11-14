using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IEvent : IRepository<Event>
    {
        int CreateScheduledEvent(Event @event);
        int CreateInfinityEvent(Event @event);
        IEnumerable<Event> UpdateInfinityEvent(Event @newEvent);
        IEnumerable<Event> UpdateScheduledEvent(Event @newEvent);
    }
}