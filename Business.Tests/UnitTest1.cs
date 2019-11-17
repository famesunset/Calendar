using Business.Models;
using Business.Services.Calendar;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly CalendarService calendarService  = new CalendarService();
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
    }
}
