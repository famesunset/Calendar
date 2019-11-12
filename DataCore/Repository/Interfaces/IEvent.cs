using System.Collections.Generic;

namespace DataCore.Repository.Interfaces
{
    public interface IEvent : IRepository<Event>
    {
        IEnumerable<Event> CreateScheduledEvent(Event @event);
        IEnumerable<Event> CreateInfinityEvent(Event @event);
    }
}