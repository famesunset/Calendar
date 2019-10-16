using System;
using Business_Layer.Models;
using System.Collections.Generic;

namespace Business_Layer
{
    public class Logic : ILogic
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
            // TODO: Invoke AddEvent() from DataLayer
            return -1;
        }
    }
}
