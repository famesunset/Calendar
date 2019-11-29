namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Linq;
    using Business.Tests.FakeRepositories.Models;
    using Data.Repository.Interfaces;

    public class FakeNotificationRepository : INotification
    {
        public void CreateNotification(int eventId, int before, int timeUnitId)
        {
            var notification = new FakeNotification
            {
                EventId = eventId,
                Before = before,
                TimeUnit = (Business.Models.NotifyTimeUnit)Enum.ToObject(typeof(Business.Models.NotifyTimeUnit), timeUnitId),
            };
            var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            fakeEvent.Notification = notification;
        }

        public void DeleteNotification(int eventId)
        {
            var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            fakeEvent.Notification.TimeUnit = Business.Models.NotifyTimeUnit.NoNotify;
        }

        public void UpdateNotification(int eventId, int before, int timeUnitId)
        {
            var fakeEvent = FakeRepository.Get.Events.SingleOrDefault(e => e.Id.Equals(eventId));
            fakeEvent.Notification.TimeUnit = (Business.Models.NotifyTimeUnit)Enum.ToObject(typeof(Business.Models.NotifyTimeUnit), timeUnitId);
            fakeEvent.Notification.Before = before;
        }
    }
}