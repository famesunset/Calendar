using Business.Models;
using Business.Services.Calendar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly CalendarService calendarService = new CalendarService();

        [TestMethod]
        public void TestCreateCalendar()
        {
            var id = calendarService.CreateCalendar(null, new Calendar {Access = Access.Private, Name = "Cal0001"});
            if (id < 1)
            {
                Assert.Fail();
            }
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
    }
}
