using System;
using System.Collections.Generic;
using Data.Models;
using Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Tests
{

    [TestClass]
    public class DataLayerTest1
    {
        private EventRepo eventRepo = new EventRepo();
        [TestMethod]
        public void TestCreateScheduledEvent()
        {
            List<EventSchedule> eventSchedule = new List<EventSchedule>();
            eventSchedule.Add(new EventSchedule(DateTime.Now, DateTime.Now));
            Event even = new Event(2, "Description2", "Notification2", "Title2", eventSchedule, DateTime.Now, DateTime.Now, true);
            eventRepo.CreateScheduledEvent(even);
        }

        [TestMethod]
        public void TestCreateInfinityEvent()
        {
            Event even = new Event(2, "Description2", "Notification2", "Title2", 30, DateTime.Now, DateTime.Now, true);
            eventRepo.CreateInfinityEvent(even);
        }
    }
}