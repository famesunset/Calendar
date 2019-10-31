﻿using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Layer.Repository;
using Data_Layer.Repository.Interfaces;

namespace Business_Layer.Services
{
    public class EventService : IEventService
    {
        public int AddEvent(string session, int calendarId, Event @event)
        {
            IEvent eventRepos = new EventRepo();
            Data_Layer.Event dataEvent = Mapper.MapBussinesEvent(@event, calendarId);
            eventRepos.AddEvent(0, dataEvent.Notification, dataEvent.Description, dataEvent.Title);
            // FIXME: тут падает
            return -1;
        }

        public Event GetEvent(string session, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEvents(string session, ICollection<int> calendarIds, DateTime beginning, DateUnit dateUnit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAllEvents(string session, DateTime beginning, DateUnit dateUnit)
        {
            throw new NotImplementedException();

        }
    }
}