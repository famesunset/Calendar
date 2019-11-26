namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeNotificationRepository : INotification
    {
        public IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNotificationInfinity(int eventId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNotificationSchedule(int eventScheduleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> UpdateNotification(int eventId, int minutesBefore)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> UpdateNotificationInfinity(int eventId, int minutesBefore)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> UpdateNotificationSchedule(int eventScheduleId, DateTime notificationTime)
        {
            throw new NotImplementedException();
        }
    }
}