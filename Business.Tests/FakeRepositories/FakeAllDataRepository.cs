﻿using System.Threading;
using Business.Tests.FakeRepositories.Models;

namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeAllDataRepository : IAllData
    {
        public IEnumerable<Data.Models.AllData> GetInfinityEvents(int userId, IEnumerable<Calendar> @calendarsList, DateTime date)
        {
            return new List<AllData>();
        }
        public IEnumerable<AllData> GetDataEvents(int userId, IEnumerable<Calendar> calendarsList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            var calendars = FakeRepository.Get.Calendars
              .Where(
                c => calendarsList.Any(cl => cl.Id.Equals(c.Id)) &&
                c.Users.Any(u => u.Id.Equals(userId))
              );

            var events = calendars.SelectMany(c => c.Events)
              .Where(e => e.Start >= dateTimeStart && e.Finish <= dateTimeFinish);

            return events.Select(EventToAllDataConverter);
        }

        public AllData GetEvent(int eventId)
        {
            var _event = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            if (_event != null)
            {
                return EventToAllDataConverter(_event);
            }

            return null;
        }

        private AllData EventToAllDataConverter(FakeEvent _event)
        {
            return new AllData
            {
                EventId = _event.Id,
                CalendarId = _event.Calendar.Id,
                Description = _event.Description,
                Title = _event.Title,
                AccessName = _event.Calendar.Access.ToString(),
                AllDay = _event.IsAllDay,
                TimeStart = _event.Start,
                TimeFinish = _event.Finish,
                CalendarName = _event.Calendar.Name,
            };
        }
    }
}