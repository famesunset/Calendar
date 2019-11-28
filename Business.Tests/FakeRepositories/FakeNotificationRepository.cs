namespace Business.Tests.FakeRepositories
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Data.Repository.Interfaces;

    public class FakeNotificationRepository : INotification
    {
        public void CreateNotification(int eventId, int before, int timeUnitId)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotification(int eventId)
        {
            throw new NotImplementedException();
        }

        public void UpdateNotification(int eventId, int before, int timeUnitId)
        {
            throw new NotImplementedException();
        }
    }
}