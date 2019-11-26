using System.Linq;
using Business.Tests.FakeRepositories.Models;

namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeCalendarRepository : ICalendar
    {
        private static int _fakeCalendarId;

        public bool CheckDefaultCalendar(int idCalendar)
        {
            throw new NotImplementedException();
        }

        public int CreateCalendar(int userId, Calendar calendar)
        {
            var fakeUser = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (fakeUser != null)
            {
                var fakeCalendar = new FakeCalendar
                {
                    Id = ++_fakeCalendarId,
                    Access = (FakeAccess)Enum.ToObject(typeof(FakeAccess), calendar.AccessId),
                    Name = calendar.Name,
                    Owner = fakeUser,
                    Events = new List<FakeEvent>(),
                    Users = new List<FakeUser>(),
                };
                fakeCalendar.Users.Add(fakeUser);
                FakeRepository.Get.Calendars.Add(fakeCalendar);
                return fakeCalendar.Id;
            }

            return -1;
        }

        public Calendar GetCalendarById(int calendarId)
        {
            var fakeCalendar = GetFakeCalendarById(calendarId);
            return FakeCalendarToCalendar(fakeCalendar);
        }

        public IEnumerable<Calendar> GetUserCalendars(int userId)
        {
            var fakeUser = FakeRepository.Get.Users.SingleOrDefault(u => u.Id.Equals(userId));
            return fakeUser?.Calendars.Select(FakeCalendarToCalendar);
        }

        public IEnumerable<User> GetUsersByCalendarId(int calendarId)
        {
            var fakeCalendar = GetFakeCalendarById(calendarId);
            return fakeCalendar?.Users.Select(FakeUserToUser);
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
            throw new NotImplementedException();
        }

        public void UnsubscribeUserFromCalendar(int userId, int calendarId)
        {
            throw new NotImplementedException();
        }

        private Calendar FakeCalendarToCalendar(FakeCalendar fakeCalendar)
        {
            if (fakeCalendar != null)
            {
                return new Calendar
                {
                    AccessId = (int)fakeCalendar.Access,
                    Name = fakeCalendar.Name,
                    Id = fakeCalendar.Id
                };
            }

            return null;
        }

        private User FakeUserToUser(FakeUser fakeUser)
        {
            if (fakeUser != null)
            {
                return new User
                {
                    IdUser = fakeUser.Id,
                    Name = fakeUser.Name,
                    Email = fakeUser.Email,
                    Mobile = fakeUser.Mobile,
                    Picture = fakeUser.PictureURL,
                    IdIdentity = fakeUser.IdentityId,
                    IdCalendarDefault = fakeUser.DefaultCalendar.Id
                };
            }

            return null;
        }

        private FakeCalendar GetFakeCalendarById(int calendarId)
        {
            return FakeRepository.Get.Calendars.SingleOrDefault(c => c.Id.Equals(calendarId));
        }
    }
}