namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Data.Repository;
    using Data.Repository.Interfaces;
    using System.Linq;
    using static Mapper;

    public class EventService : IEventService
    {
        public int AddEvent(string session, Event @event)
        {
            IEvent eventRepos = new EventRepo();
            Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(@event);
            int eventId = eventRepos.CreateScheduledEvent(dataEvent);
            return eventId;
        }

        public Event GetEvent(string session, int id)
        {
            var userRepos = new UserRepo();
            // получить юзера по сесии

            var dataRepository = new AllDataRepo();
            var dataEvent = dataRepository.GetEvent(id);
            var businessEvent = Map.Map<Data.Models.AllData, Event>(dataEvent);
            return businessEvent;
        }

        public IEnumerable<Calendar> GetEvents(string session, DateTime beginning, DateUnit dateUnit)
        {
            var eventRepos = new AllDataRepo();
            var calendarRepos = new CalendarRepo();
            // var userRepos = new UserRepo();
            var userId = 1; // получить юзера по сесии

            DateTime dateStart = new DateTime();
            DateTime dateFinish = new DateTime();
            beginning = beginning.ToUniversalTime();
            switch (dateUnit)
            {
                case DateUnit.Day:
                {
                    dateStart = beginning;
                    dateFinish = dateStart.AddDays(1);
                }
                break;
                case DateUnit.Month:
                {
                    dateStart = new DateTime(beginning.Year, beginning.Month, 1).ToUniversalTime();
                    dateFinish = dateStart.AddMonths(1);
                }
                break;
                case DateUnit.Week:
                {
                    int startDay = beginning.Day - (int)beginning.DayOfWeek;
                    dateStart = new DateTime(beginning.Year, beginning.Month, startDay);
                    dateFinish = dateStart.AddDays(7);
                }
                break;
            }

            var userCalendars = calendarRepos.GetUserCalendars(userId);
            var events = eventRepos.GetDataEvents(userId, userCalendars, dateStart, dateFinish);
            var bUserCalendars = userCalendars
                .Select(c => Map.Map<Data.Models.Calendar, Calendar>(c))
                .ToDictionary(c => c.Id);

            foreach (var e in events)
            {
                var bEvent = Map.Map<Data.Models.AllData, BaseEvent>(e);
                bUserCalendars[e.IdCalendar].Events.Add(bEvent);
            }

            return bUserCalendars.Values;
        }

        public int CreateScheduledEvent(string session, Event @event)
        {
            IEvent eventRepos = new EventRepo();
            List<EventSchedule> schedule = new List<EventSchedule>();

            int compare;
            switch (@event.Repeat)
            {
                case Interval.Day:
                    do
                    {
                        compare = DateTime.Compare(@event.Start, @event.Finish);
                        schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                        @event.Start = @event.Start.AddDays(1);
                        @event.Finish = @event.Finish.AddDays(1);
                    }
                    while (compare < 0);
                    break;
                case Interval.Week:
                    do
                    {
                        compare = DateTime.Compare(@event.Start, @event.Finish);
                        schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                        @event.Start = @event.Start.AddDays(7);
                        @event.Finish = @event.Finish.AddDays(7);
                    }
                    while (compare < 0);
                    break;
                case Interval.Month:
                    do
                    {
                        compare = DateTime.Compare(@event.Start, @event.Finish);
                        schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                        @event.Start = @event.Start.AddMonths(1);
                        @event.Finish = @event.Finish.AddMonths(1);
                    }
                    while (compare < 0);
                    break;
                case Interval.Year:
                    do
                    {
                        compare = DateTime.Compare(@event.Start, @event.Finish);
                        schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                        @event.Start = @event.Start.AddYears(1);
                        @event.Finish = @event.Finish.AddYears(1);
                    }
                    while (compare < 0);
                    break;
            }
            @event.Schedule = schedule;
            Data.Models.Event dataEvent = Mapper.Map.Map<Event, Data.Models.Event>(@event);
            int eventId = eventRepos.CreateScheduledEvent(dataEvent);
            return eventId;
        }

        public void DeleteEvent(string session, int eventId)
        {
            var eventRepos = new EventRepo();
            var userRepos = new UserRepo();
            // получить юзера по сесии

            eventRepos.Delete(eventId);
        }

        public void UpdateInfinityEvent(Event @newEvent)
        {
            IEvent eventRepos = new EventRepo();
            Data.Models.Event dataEvent = Mapper.Map.Map<Event, Data.Models.Event>(@newEvent);
            eventRepos.UpdateInfinityEvent(dataEvent);
        }

        public void UpdateScheduledEvent(Event @newEvent)
        {
            IEvent eventRepos = new EventRepo();
            Data.Models.Event dataEvent = Mapper.Map.Map<Event, Data.Models.Event>(@newEvent);
            eventRepos.UpdateScheduledEvent(dataEvent);
        }
    }
}