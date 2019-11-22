using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IEvent
    {
        int CreateEvent(Event @event);
        void UpdateEvent(Event @newEvent);
        void Delete(int eventId);
    }
}