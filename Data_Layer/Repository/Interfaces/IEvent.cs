using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repository.Interfaces
{
    interface IEvent : IRepository<Event>
    {
        IEnumerable<Event> AddEvent(int id, string notification, string description, string title);
        IEnumerable<Event> CreateScheduledEvent(int id, string notification, string description, string title,
            DateTime timeStart, DateTime timeFinish);
    }
}
