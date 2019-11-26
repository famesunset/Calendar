using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            eventRepo.CreateEvent(even);
        }

        [TestMethod]
        public void TestCreateInfinityEvent()
        {
            Event even = new Event(2, "Description2", "Notification2", "Title2", 30, DateTime.Now, DateTime.Now, true);
            eventRepo.CreateEvent(even);
        }

        [TestMethod]
        public void TestGetColorById()
        {
            ColorRepo colorRepo = new ColorRepo();
            var a = colorRepo.GetColorById(1);
        }

        [TestMethod]
        public void TestDeleteNotificationInfinity()
        {
            NotificationRepo notificationRepo = new NotificationRepo();
            notificationRepo.DeleteNotificationInfinity(1);
        }

        [TestMethod]
        public void DeleteNotificationSchedule()
        {
            NotificationRepo notificationRepo = new NotificationRepo();
            notificationRepo.DeleteNotificationSchedule(1);
        }
    }
}
