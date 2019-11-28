namespace Business.Services.Event
{
    using Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;

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
                int eventId = eventRepos.CreateEvent(dataEvent);
                if (@event.Notify != null && @event.Notify.Value > 0)
                {
                    notificationRepos.CreateNotification(eventId, @event.Notify.Value, (int)@event.Notify.TimeUnit);
                }

                return eventId;
            }
            return -1;
        }

        public Event GetEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                var businessEvent = Mapper.Map<Data.Models.AllData, Event>(dataBigEvent);
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

                var userCalendars = calendarRepos
                    .GetUserCalendars(dataUser.IdUser)
                    .Where(uc => calendarIds.Any(ci => ci.Equals(uc.Id)))
                    .ToList();

                if (userCalendars.Count() > 0)
                {
                    var events = bigEventRepos.GetDataEvents(dataUser.IdUser, userCalendars, dateRange.Start, dateRange.Finish);
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

        public void DeleteEvent(string loginedUserId, int eventId)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, eventId);
            if (dataBigEvent != null)
            {
                eventRepos.Delete(dataBigEvent.EventId);
                notificationRepos.DeleteNotification(dataBigEvent.EventId);
            }

        }

        public void UpdateEvent(string loginedUserId, Event newEvent)
        {
            var dataBigEvent = serviceHelper.IsUserHasAccessToEvent(loginedUserId, newEvent.Id);
            if (dataBigEvent != null)
            {
                Data.Models.Event dataEvent = Mapper.Map<Event, Data.Models.Event>(newEvent);
                eventRepos.UpdateEvent(dataEvent);
                if (newEvent.Notify != null)
                {
                    notificationRepos.UpdateNotification(dataBigEvent.EventId, newEvent.Notify.Value, (int)newEvent.Notify.TimeUnit);
                }
            }
        }
    }
}
