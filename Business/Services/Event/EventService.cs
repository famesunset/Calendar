namespace Business.Services.Event
{
    using Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;
    using Newtonsoft.Json;
    using static AMapper;

    public partial class EventService : IEventService
    {
        private readonly IEvent eventRepos;
        private readonly ICalendar calendarRepos;
        private readonly IAllData bigEventRepos;
        private readonly INotification notificationRepos;

        private readonly ServiceHelper serviceHelper;

        public EventService(IEvent eventRepository, ICalendar calendarRepository,
            IAllData bigEventRepository, IUser userRepository, INotification notificationRepository)
        {
            this.eventRepos = eventRepository;
            this.calendarRepos = calendarRepository;
            this.bigEventRepos = bigEventRepository;
            this.notificationRepos = notificationRepository;

            this.serviceHelper = new ServiceHelper(userRepository, calendarRepository, bigEventRepository);
        }

        public int CreateEvent(string loginedUserId, Event @event, int timeOffset)
        {
            var dataCalendar = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, @event.CalendarId);
            if (dataCalendar != null)
            {
                if (!@event.Start.Kind.Equals(DateTimeKind.Utc))
                {
                    @event.Start = @event.Start.AddMinutes(timeOffset);
                    @event.Finish = @event.Finish.AddMinutes(timeOffset);
                }

                Data.Models.Event dataEvent = Mapper.Map<Event, Data.Models.Event>(@event);
                int eventId = serviceHelper.WrapMethodWithReturn(() => eventRepos.CreateEvent(dataEvent), -1);
                if (eventId > 0)
                {
                    if (@event.Notify != null && @event.Notify.Value > 0)
                    {
                        notificationRepos.CreateNotification(eventId, @event.Notify.Value, (int)@event.Notify.TimeUnit);
                    }
                }
                return eventId;
            }
            return -1;
        }

        public Event GetEvent(string loginedUserId, int eventId, int timeOffset)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                var businessEvent = Mapper.Map<Data.Models.AllData, Event>(dataBigEvent);
                AddTimeOffset(businessEvent, timeOffset);
                return businessEvent;
            }
            return null;
        }

        public IEnumerable<BaseEvent> GetEvents(string loginedUserId, DateTime beginning, DateUnit dateUnit, int[] calendarIds, int timeOffset)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                beginning = beginning.Date;
                var dateRange = GetDateRange(beginning, dateUnit);

                var userCalendars = serviceHelper.WrapMethodWithReturn(() => calendarRepos.GetUserCalendars(dataUser.IdUser), 
                    new List<Data.Models.Calendar>())
                    .Where(uc => calendarIds.Any(ci => ci.Equals(uc.Id)));

                if (userCalendars.Any())
                {
                    var events = serviceHelper.WrapMethodWithReturn(() => bigEventRepos.GetDataEvents(dataUser.IdUser, userCalendars, dateRange.Start, dateRange.Finish), 
                        new List<Data.Models.AllData>());
                    foreach (var _event in events)
                    {
                        AddTimeOffset(_event, timeOffset);
                    }
                    events = events.Concat(GetInfinityEvents(dataUser.IdUser, userCalendars, beginning, dateUnit, dateRange.Item2, timeOffset));
                    return events.Select(e => Mapper.Map<Data.Models.AllData, BaseEvent>(e));
                }
            }
            return new List<BaseEvent>();
        }

        public bool DeleteEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                var success = serviceHelper.WrapMethod(() => eventRepos.Delete(dataBigEvent.EventId));
                if (success)
                {
                    success = serviceHelper.WrapMethod(() => notificationRepos.DeleteNotification(dataBigEvent.EventId));
                }

                return success;
            }
            return false;
        }

        public bool UpdateEvent(string loginedUserId, Event newEvent)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, newEvent.Id);
            if (dataBigEvent != null)
            {
                Data.Models.Event dataEvent = Mapper.Map<Event, Data.Models.Event>(newEvent);
                var success = serviceHelper.WrapMethod(() => eventRepos.UpdateEvent(dataEvent));
                if (newEvent.Notify != null && success)
                {
                    success = serviceHelper.WrapMethod(() => 
                        notificationRepos.UpdateNotification(dataBigEvent.EventId, newEvent.Notify.Value, (int)newEvent.Notify.TimeUnit));
                }

                return success;
            }

            return false;
        }

        public string GetEventLink(string loginedUserId, int eventId, string domain, int timeOffset)
        {
            var _event = GetEvent(loginedUserId, eventId, timeOffset);
            return $"{domain}/?event={JsonConvert.SerializeObject(_event)}";
        }
    }
}
