using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Layer.Repository;
using Data_Layer.Repository.Interfaces;

namespace Business_Layer.Services
{
    public class EventService : IEventService
    {
        public Event GetEvent(string session, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEvents(string session, ICollection<int> calendarIds, DateUnit dateUnit, DateTime beginning)
        {
            throw new NotImplementedException();
        }

        public int AddEvent(string session, Event @event)
        {
            IEvent eventRepos = new EventRepo();

            Data_Layer.Event dataEvent = Mapper.Map.Map<Event, Data_Layer.Event>(@event);
            eventRepos.AddEvent(@event.Id, null, @event.Description, @event.Title);


            return -1;
        }
    }
}