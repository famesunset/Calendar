using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IEvent
    {
        //int CreateScheduledEvent(Event @event);
        int CreateInfinityEvent(Event @event);
        void UpdateInfinityEvent(Event @newEvent);
        //void UpdateScheduledEvent(Event @newEvent);
        void Delete(int eventId);
    }
}