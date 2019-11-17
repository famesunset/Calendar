namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Data.Repository;
    using Data.Repository.Interfaces;
    using System.Linq;
    using static Mapper;

    public partial class EventService : IEventService
    {
        private readonly IEvent eventRepos;
        private readonly IUser userRepos;
        private readonly ICalendar calendarRepos;
        private readonly IAccess accessRepos;
        private readonly INotification notificationRepos;
        private readonly IAllData bigEventRepos;

        public EventService()
        {
            eventRepos = new EventRepo();
            userRepos = new UserRepo();
            calendarRepos = new CalendarRepo();
            accessRepos = new AccessRepo();
            notificationRepos = new NotificationRepo();
            bigEventRepos = new AllDataRepo();
        }

        public int CreateEvent(string loginedUserId, Event @event)
        {
            if (IsUserHasAccessToCalendar(loginedUserId, @event.CalendarId))
            {
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(@event);
                int eventId = eventRepos.CreateScheduledEvent(dataEvent);
                return eventId;
            }
            return -1;
        }

        public Event GetEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                var businessEvent = Map.Map<Data.Models.AllData, Event>(dataBigEvent);
                return businessEvent;
            }
            return null;
        }

        public IEnumerable<Calendar> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit)
        {
            var dataUser = IsUserLoggined(loginedUserId);
            if (dataUser != null)
            {
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
                    case DateUnit.Week:
                        {
                            int startDay = beginning.Day - (int)beginning.DayOfWeek;
                            dateStart = new DateTime(beginning.Year, beginning.Month, startDay);
                            dateFinish = dateStart.AddDays(7);
                        }
                        break;
                    case DateUnit.Month:
                        {
                            dateStart = new DateTime(beginning.Year, beginning.Month, 1);
                            dateFinish = dateStart.AddMonths(1);
                        }
                        break;
                }

                var userCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                if (userCalendars.Count() > 0)
                {
                    var events = bigEventRepos.GetDataEvents(dataUser.IdUser, userCalendars, dateStart, dateFinish);
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
                return new List<Calendar>();

            }

            return null;
        }

        public int CreateScheduledEvent(string loginedUserId, Event @event)
        {
            if (IsUserHasAccessToCalendar(loginedUserId, @event.CalendarId))
            {
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
                        } while (compare < 0);

                        break;
                    case Interval.Week:
                        do
                        {
                            compare = DateTime.Compare(@event.Start, @event.Finish);
                            schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                            @event.Start = @event.Start.AddDays(7);
                            @event.Finish = @event.Finish.AddDays(7);
                        } while (compare < 0);

                        break;
                    case Interval.Month:
                        do
                        {
                            compare = DateTime.Compare(@event.Start, @event.Finish);
                            schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                            @event.Start = @event.Start.AddMonths(1);
                            @event.Finish = @event.Finish.AddMonths(1);
                        } while (compare < 0);

                        break;
                    case Interval.Year:
                        do
                        {
                            compare = DateTime.Compare(@event.Start, @event.Finish);
                            schedule.Add(new EventSchedule(@event.Start, @event.Finish));
                            @event.Start = @event.Start.AddYears(1);
                            @event.Finish = @event.Finish.AddYears(1);
                        } while (compare < 0);

                        break;
                }

                @event.Schedule = schedule;
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(@event);
                int eventId = eventRepos.CreateScheduledEvent(dataEvent);
                return eventId;
            }

            return -1;
        }

        public void DeleteEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                eventRepos.Delete(dataBigEvent.EventId);
            }

        }

        public void UpdateInfinityEvent(string loginedUserId, Event newEvent)
        {
            var dataBigEvent = IsUserHasAccessToEvent(loginedUserId, newEvent.Id);
            if (dataBigEvent != null)
            {
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(newEvent);
                eventRepos.UpdateInfinityEvent(dataEvent);
            }
        }

        public void UpdateScheduledEvent(string loginedUserId, Event newEvent)
        {
            var dataBigEvent = IsUserHasAccessToEvent(loginedUserId, newEvent.Id);
            if (dataBigEvent != null)
            {
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(newEvent);
                eventRepos.UpdateScheduledEvent(dataEvent);
            }
        }
    }
}