using System;
using System.Collections.Generic;
using Business.Models;
using Business.Services.Calendar;
using Business.Services.Event;
using Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly CalendarService calendarService = new CalendarService(new CalendarRepo(), new UserRepo(), new AllDataRepo(), new ColorRepo());

        [TestMethod]
        public void TestCreateCalendar()
        {
            //var id = calendarService.CreateCalendar(null, new Calendar {Access = Access.Private, Name = "Cal0001"});
            //if (id < 1)
            //{
            //    Assert.Fail();
            //}
        }

        [TestMethod]
        public void TestGetCalendar()
        {
            var calendar = calendarService.GetCalendar(null, 2);
            if (calendar is null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGetCalendars()
        {
            var calendars = calendarService.GetCalendars(null);
            if (calendars is null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestRemoveCalendar()
        {
            User user = new User();
            user.IdentityId = "a1d4e615-1b09-49af-b30f-5314f4652513";
            Calendar calendar = new Calendar();
            calendar.Id = 61;
            calendar.UserOwnerId = 4;
            calendarService.DeleteCalendar(user.IdentityId, calendar.Id);
        }

        [TestMethod]
        public void TestBuildInfinityEvents()
        {
            //User user = new User();
            //user.Id = 39;
            //var calendar = new List<Data.Models.Calendar>();
            //var t = new Data.Models.Calendar(accessId: 1, id: 33, name: "#2 Share");
            //calendar.Add(t);
            //EventService ev = new EventService();
            //DateTime dateStart = new DateTime(2019, 11,25);
            //DateTime dateFinish = new DateTime(2019, 11, 28);
            //ev.BuildInfinityEvents(39, calendar, dateStart, dateFinish);
        }
    }
}
