using System;
using Business_Layer.Models;
using System.Collections.Generic;
using static Business_Layer.Mapper;

namespace Business_Layer
{
    public class Service : IService
    {
        private const bool Debug = true;

        public IEnumerable<Calendar> GetCalendars(string session)
        {
            throw new NotImplementedException();
        }

        public Calendar GetCalendar(string session, int calendarId)
        {
            throw new NotImplementedException();
        }

        public int AddCalendar(string session, Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public Event GetEvent(string session, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEvents(string session, int calendarId)
        {
            throw new NotImplementedException();
        }

        public int AddEvent(string session, Event @event)
        {
            if (!Debug)
            {
                // TODO: Get user by session
            }
            // TODO: Map BusinessEvent to DataEvent
            Data_Layer.Event dataEvent = Map.Map<Event, Data_Layer.Event>(@event);
            // TODO: Invoke AddEvent() from DataLayer
            return -1;
        }
    }
}
