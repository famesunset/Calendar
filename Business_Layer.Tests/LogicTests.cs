using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business_Layer.Tests
{
    [TestClass]
    public class LogicTests
    {
        private IService service = new Service();
        [TestMethod]
        public void TestMethod1()
        {
            service.AddEvent(null, new Models.Event { CalendarId = 2, Description = "Desc", Title = "Calendar00" });
        }

        [TestMethod]
        public void TestSetUserToCalendar()
        {
            service.AddCalendar(null, new Models.Calendar { Id = 2 });
        }
    }
}
