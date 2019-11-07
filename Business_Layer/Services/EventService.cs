using System;
using System.Collections.Generic;
using Business_Layer.Models;
using Data_Layer.Repository;
using Data_Layer.Repository.Interfaces;
using static Business_Layer.Mapper;

namespace Business_Layer.Services
{
    public class EventService : IEventService
    {
        public int AddEvent(string session, int calendarId, Event @event)
        {
            IEvent eventRepos = new EventRepo();
            Data_Layer.Event dataEvent = Mapper.MapBussinesEvent(@event, calendarId);
            //eventRepos.AddEvent(0, dataEvent.Notification, dataEvent.Description, dataEvent.Title);
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
            var dateStart = new DateTime(beginning.Year, beginning.Month, 1);
            var dateEnd = dateStart.AddMonths(1);
            var repos = new AllDataRepo();
            List<Data_Layer.Calendar> cals = new List<Data_Layer.Calendar>();
            cals.Add(new Data_Layer.Calendar(2));
            Data_Layer.User user = new Data_Layer.User(1);
            var events = repos.GetDataEvents(user, cals, dateStart, dateEnd);
            var eventList = new List<Event>();

            foreach (var allData in events)
            {
                var bEvent = Map.Map<Data_Layer.Models.AllData, Event>(allData);
                eventList.Add(bEvent);
            }

            return eventList;
        }

        public void CreateScheduledEvent(Event @event)
        {
            IEvent eventRepos = new EventRepo();

            Data_Layer.Event dataEvent = Mapper.Map.Map<Event, Data_Layer.Event>(@event);
            List<EventSchedule> schedule = new List<EventSchedule>();

            int compare;
            switch (@event.Repeat)
            {
                case Interval.Day:
                    do
                    {
                        compare = DateTime.Compare(@event.TimeEventStart, @event.DateFinish);
                        schedule.Add(new EventSchedule(@event.TimeEventStart, @event.TimeEventFinish));
                        @event.TimeEventStart = @event.TimeEventStart.AddDays(1);
                        @event.TimeEventFinish = @event.TimeEventFinish.AddDays(1);
                    }
                    while (compare < 0);
                    break;
                case Interval.Week:
                    do
                    {
                        compare = DateTime.Compare(@event.TimeEventStart, @event.DateFinish);
                        schedule.Add(new EventSchedule(@event.TimeEventStart, @event.TimeEventFinish));
                        @event.TimeEventStart = @event.TimeEventStart.AddDays(7);
                        @event.TimeEventFinish = @event.TimeEventFinish.AddDays(7);
                    }
                    while (compare < 0);
                    break;
                case Interval.Month:
                    do
                    {
                        compare = DateTime.Compare(@event.TimeEventStart, @event.DateFinish);
                        schedule.Add(new EventSchedule(@event.TimeEventStart, @event.TimeEventFinish));
                        @event.TimeEventStart = @event.TimeEventStart.AddMonths(1);
                        @event.TimeEventFinish = @event.TimeEventFinish.AddMonths(1);
                    }
                    while (compare < 0);
                    break;
                case Interval.Year:
                    do
                    {
                        compare = DateTime.Compare(@event.TimeEventStart, @event.DateFinish);
                        schedule.Add(new EventSchedule(@event.TimeEventStart, @event.TimeEventFinish));
                        @event.TimeEventStart = @event.TimeEventStart.AddYears(1);
                        @event.TimeEventFinish = @event.TimeEventFinish.AddYears(1);
                    }
                    while (compare < 0);
                    break;
            }
            @event.Schedule = schedule;
            // eventRepos.CreateScheduledEvent(@event);
        }
    }
}