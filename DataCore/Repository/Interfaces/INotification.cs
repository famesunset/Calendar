using System;
using System.Collections.Generic;

namespace DataCore.Repository.Interfaces
{
    public interface INotification : IRepository<Notification>
    {
        IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime);
    }
}