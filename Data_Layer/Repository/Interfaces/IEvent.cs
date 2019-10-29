using System;
using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface IEvent : IRepository<Event>
    {
        IEnumerable<Event> AddEvent(int id, string notification, string description, string title);
        IEnumerable<Event> CreateScheduledEvent(int id, string notification, string description, string title,
            DateTime timeStart, DateTime timeFinish);
    }
}
