namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Data.Repository.Interfaces;
    using Business.Tests.FakeRepositories.Models;

    public class FakeAllDataRepository : IAllData
    {
       
        public IEnumerable<AllData> GetDataEvents(int userId, IEnumerable<Calendar> calendarsList, DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            var calendars = FakeRepository.Get.Calendars
              .Where(
                c => calendarsList.Any(cl => cl.Id.Equals(c.Id)) &&
                c.Users.Any(u => u.Id.Equals(userId))
              );

            var events = calendars.SelectMany(c => c.Events)
              .Where(e => e.Start >= dateTimeStart && e.Finish <= dateTimeFinish && e.Interval == Business.Models.Interval.NoRepeat);

            return events.Select(FakeConverters.EventToAllDataConverter);
        }

        public AllData GetEvent(int eventId)
        {
            var _event = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            if (_event != null)
            {
                return FakeConverters.EventToAllDataConverter(_event);
            }

            return null;
        }

        public IEnumerable<AllData> GetInfinityEvents(int userId, IEnumerable<Calendar> calendarsList, DateTime finish)
        {
            var calendars = FakeRepository.Get.Calendars
              .Where(
                c => calendarsList.Any(cl => cl.Id.Equals(c.Id)) &&
                c.Users.Any(u => u.Id.Equals(userId))
              );

            var events = calendars.SelectMany(c => c.Events)
              .Where(e => e.Interval != Business.Models.Interval.NoRepeat);

            return events.Select(FakeConverters.EventToAllDataConverter);
        }

       
    }
}