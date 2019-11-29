using Business.Tests.FakeRepositories.Models;
using Data.Models;
using System;
using System.Collections.Generic;

namespace Business.Tests.FakeRepositories
{
    public static class FakeConverters
    {
        public static FakeUser UserToFakeUser(User user)
        {
            if (user != null)
            {
                return new FakeUser
                {
                    Id = user.IdUser,
                    Browsers = new List<FakeBrowser>(),
                    Calendars = new List<FakeCalendar>(),
                    Email = user.Email,
                    IdentityId = user.IdIdentity,
                    Mobile = user.Mobile,
                    Name = user.Name,
                    PictureURL = user.Picture,
                };
            }
            return null;
        }
        public static Browser FakeBrowserToBrowser(FakeBrowser fakeBrowser)
        {
            if (fakeBrowser != null)
            {
                return new Browser
                {
                    Id = fakeBrowser.Id,
                    BrowserId = fakeBrowser.BrowserId,
                    UserId = fakeBrowser.User.Id,
                };
            }
            return null;
        }

        public static Calendar FakeCalendarToCalendar(FakeCalendar fakeCalendar)
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

        public static FakeCalendar CalendarToFakeCalendar(Calendar calendar, FakeUser fakeUser)
        {
            if (calendar != null)
            {
                return new FakeCalendar
                {
                    Id = calendar.Id,
                    Access = (Business.Models.Access)Enum.ToObject(typeof(Business.Models.Access), calendar.AccessId),
                    Name = calendar.Name,
                    Owner = fakeUser,
                    Events = new List<FakeEvent>(),
                    Users = new List<FakeUser>(),
                    Color = new ColorFake { Id = calendar.ColorId, Hex = calendar.ColorHex },
                };
            }
            return null;
        }

        public static User FakeUserToUser(FakeUser fakeUser)
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

        public static AllData EventToAllDataConverter(FakeEvent _event)
        {
            if (_event != null)
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
                    CalendarColor = _event.Calendar.Color.Hex,
                    RepeatId = (int)_event.Interval,
                    NotificationValue = _event.Notification.Before,
                    NotificationTimeUnitId = (int)_event.Notification.TimeUnit,
                };
            }
            return null;
        }

        public static FakeEvent EventToFakeEvent(Event _event, FakeCalendar fakeCalendar)
        {
            return new FakeEvent
            {
                Id = _event.Id,
                Calendar = fakeCalendar,
                Description = _event.Description,
                Start = _event.TimeStart,
                Finish = _event.TimeFinish,
                Title = _event.Title,
                IsAllDay = _event.AllDay,
            };
        }
    }
}
