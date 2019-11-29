namespace Business.Tests.FakeRepositories
{
    using System.Linq;
    using Business.Tests.FakeRepositories.Models;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repository.Interfaces;
    public class FakeCalendarRepository : ICalendar
    {
        private static int fakeCalendarId = 0;

        public bool CheckDefaultCalendar(int idCalendar)
        {
            return FakeRepository.Get.
                Calendars.SingleOrDefault(c => c.Id.Equals(idCalendar)).
                Users.Any(u => u.DefaultCalendar.Id.Equals(idCalendar));
        }

        public int CreateCalendar(int userId, Calendar calendar)
        {
            var fakeUser = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (fakeUser != null)
            {
                calendar.Id = ++fakeCalendarId;
                var fakeCalendar = FakeConverters.CalendarToFakeCalendar(calendar, fakeUser);
                fakeCalendar.Users.Add(fakeUser);
                FakeRepository.Get.Calendars.Add(fakeCalendar);
                fakeUser.Calendars.Add(fakeCalendar);
                return fakeCalendar.Id;
            }

            return -1;
        }

        public Calendar GetCalendarById(int calendarId)
        {
            var fakeCalendar = GetFakeCalendarById(calendarId);
            return FakeConverters.FakeCalendarToCalendar(fakeCalendar);
        }

        public IEnumerable<Calendar> GetUserCalendars(int userId)
        {
            var fakeUser = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(userId));
            return fakeUser?.Calendars.Select(FakeConverters.FakeCalendarToCalendar);
        }

        public IEnumerable<User> GetUsersByCalendarId(int calendarId)
        {
            var fakeCalendar = GetFakeCalendarById(calendarId);
            return fakeCalendar?.Users.Select(FakeConverters.FakeUserToUser);
        }

        public int? RemoveCalendar(int id)
        {
            var fakeCalendar = GetFakeCalendarById(id);
            if (fakeCalendar != null)
            {
                FakeRepository.Get.Calendars.Remove(fakeCalendar);
                fakeCalendar.Users.ForEach(u => u.Calendars.Remove(fakeCalendar));
                return GetFakeCalendarById(id)?.Id;
            }

            return null;
        }

        public void SubscribeUserToCalendar(int userId, int calendarId)
        {
            var user = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(userId));
            var calendar = GetFakeCalendarById(calendarId);
            user?.Calendars.Add(calendar);
            calendar?.Users.Add(user);
        }

        public void UnsubscribeUserFromCalendar(int userId, int calendarId)
        {
            var calendar = GetFakeCalendarById(calendarId);
            var user = calendar?.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                user.Calendars.Remove(calendar);
                calendar.Users.Remove(user);
            }
        }

        private FakeCalendar GetFakeCalendarById(int calendarId)
        {
            return FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(calendarId));
        }
    }
}