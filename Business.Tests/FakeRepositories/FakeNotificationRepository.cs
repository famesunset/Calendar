

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
  }
}