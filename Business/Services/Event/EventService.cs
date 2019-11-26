namespace Business.Services.Event
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Data.Repository.Interfaces;
    using System.Linq;
    using static Mapper;

    public class EventService : IEventService
    {
        private readonly IEvent eventRepos;
        private readonly ICalendar calendarRepos;
        private readonly IAllData bigEventRepos;

        private readonly ServiceHelper serviceHelper;

        public EventService(IEvent eventRepository, ICalendar calendarRepository, IAllData bigEventRepository, IUser userRepository)
        {
            eventRepos = eventRepository;
            calendarRepos = calendarRepository;
            bigEventRepos = bigEventRepository;

            serviceHelper = new ServiceHelper(userRepository, calendarRepository, bigEventRepository);
        }

        public int CreateEvent(string loginedUserId, Event @event)
        {
            var dataCalendar = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, @event.CalendarId);
            if (dataCalendar != null)
            {
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(@event);
                int eventId = eventRepos.CreateEvent(dataEvent);
                return eventId;
            }
            return -1;
        }

        public Event GetEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                var businessEvent = Map.Map<Data.Models.AllData, Event>(dataBigEvent);
                return businessEvent;
            }
            return null;
        }

        public IEnumerable<BaseEvent> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit, int[] calendarIds = null)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                DateTime dateStart;
                DateTime dateFinish;
                beginning = beginning.ToUniversalTime();
                switch (dateUnit)
                {
                    case DateUnit.Day:
                    default:
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
                userCalendars = userCalendars.Where(uc => calendarIds.Any(ci => ci.Equals(uc.Id))).ToList();

                if (userCalendars.Count() > 0)
                {
                    var events = bigEventRepos.GetDataEvents(dataUser.IdUser, userCalendars, dateStart, dateFinish);
                    events = events.Concat(GetInfinityEvents(dataUser.IdUser, userCalendars, beginning, dateUnit, dateFinish));

                    var bUserCalendars = userCalendars
                      .Select(c => Map.Map<Data.Models.Calendar, Calendar>(c))
                      .ToDictionary(c => c.Id);

                    foreach (var e in events)
                    {
                        var bEvent = Map.Map<Data.Models.AllData, BaseEvent>(e);
                        bUserCalendars[e.CalendarId].Events.Add(bEvent);
                    }

                    return bUserCalendars.Values.SelectMany(c => c.Events);
                }
            }
            return new List<BaseEvent>();
        }

        private IEnumerable<Data.Models.AllData> GetInfinityEvents(int userId, IEnumerable<Data.Models.Calendar> calendarList, DateTime beginning, DateUnit dateUnit, DateTime finish)
        {
            var events = bigEventRepos.GetInfinityEvents(userId, calendarList, finish);
            var result = new List<Data.Models.AllData>();

            foreach (var _event in events)
            {
                switch (dateUnit)
                {
                    case DateUnit.Day:
                    default:
                        switch (_event.RepeatId)
                        {
                            case (int)Interval.Day:
                                result.Add(_event);
                                break;
                            case (int)Interval.Week:
                                if(beginning.DayOfWeek >= _event.TimeStart.DayOfWeek &&
                                    beginning.DayOfWeek <= _event.TimeFinish.DayOfWeek)
                                {
                                    result.Add(_event);
                                }
                                break;
                            case (int)Interval.Month:
                                if(beginning.Day >= _event.TimeStart.Day &&
                                    beginning.Day <= _event.TimeFinish.Day)
                                {
                                    result.Add(_event);
                                }
                                break;
                            case (int)Interval.Year:
                                if(beginning.Month >= _event.TimeStart.Month &&
                                    beginning.Month <= _event.TimeFinish.Month &&
                                    beginning.Day >= _event.TimeStart.Day &&
                                    beginning.Day <= _event.TimeFinish.Day)
                                {
                                    result.Add(_event);
                                }
                                break;
                        }
                        break;
                    case DateUnit.Week:
                        // TODO: throw new NotImplementedException();
                        break;
                    case DateUnit.Month:
                        // TODO: throw new NotImplementedException();
                        break;
                }
            }

            return result;
        }

        public IEnumerable<Data.Models.AllData> BuildInfinityEvents(int idUser, IEnumerable<Data.Models.Calendar> @calendarsList, DateTime dateStart, DateTime dateFinish)
        {
            IEnumerable<Data.Models.AllData> s = bigEventRepos.GetInfinityEvents(idUser, @calendarsList, dateFinish);
            var infinity = new List<Data.Models.AllData>();
            foreach (var t in s)
            {
                if (t.TimeStart < dateStart)
                {
                    DateTime tempStart = t.TimeStart;
                    DateTime tempFinish = t.TimeFinish;
                    switch (t.RepeatId)
                    {
                        case 1:
                            {
                                do
                                {
                                    infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
                                        t.Description, t.Title, t.EventId,
                                        tempStart, tempFinish,
                                        t.RepeatId));

                                    tempStart = tempStart.AddDays(1);
                                    tempFinish = tempFinish.AddDays(1);
                                } while (dateFinish.AddDays(1)! > tempStart);

                                break;
                            }

                        case 7:
                            {
                                do
                                {
                                    infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
                                        t.Description, t.Title, t.EventId,
                                        tempStart, tempFinish, 
                                        t.RepeatId));

                                    tempStart = tempStart.AddDays(7);
                                    tempFinish = tempFinish.AddDays(7);
                                } while (dateFinish.AddDays(1)! > tempStart);

                                break;
                            }

                        case 30:
                            {
                                do
                                {
                                    infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
                                        t.Description, t.Title, t.EventId,
                                        tempStart, tempFinish,
                                        t.RepeatId));

                                    tempStart = tempStart.AddDays(30);
                                    tempFinish = tempFinish.AddDays(30);
                                } while (dateFinish.AddDays(1)! > tempStart);

                                break;
                            }

                        case 365:
                            {
                                do
                                {
                                    infinity.Add(new Data.Models.AllData(t.CalendarId, t.CalendarName, t.AccessName, t.EventId,
                                        t.Description, t.Title, t.EventId,
                                        tempStart, tempFinish,
                                        t.RepeatId));

                                    tempStart = tempStart.AddDays(365);
                                    tempFinish = tempFinish.AddDays(365);
                                } while (dateFinish.AddDays(1)! > tempStart);

                                break;
                            }
                    }
                }
            }
            return infinity;
        }

        public void DeleteEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                eventRepos.Delete(dataBigEvent.EventId);
            }

        }

        public void UpdateEvent(string loginedUserId, Event newEvent)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, newEvent.Id);
            if (dataBigEvent != null)
            {
                Data.Models.Event dataEvent = Map.Map<Event, Data.Models.Event>(newEvent);
                eventRepos.UpdateEvent(dataEvent);
            }
        }
    }
}
