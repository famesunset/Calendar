using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business_Layer.Tests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            IService service = new Service();

            service.AddEvent(null, new Models.Event { CalendarId = 2, Description = "Desc", Title = "Calendar00" });
        }
    }
}
