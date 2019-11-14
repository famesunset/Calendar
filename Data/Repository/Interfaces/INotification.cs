using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface INotification : IRepository<Notification>
    {
        IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime);
    }
}