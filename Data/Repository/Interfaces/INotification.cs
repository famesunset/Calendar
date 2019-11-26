using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface INotification
    {
        IEnumerable<Notification> CreateNotification(int eventScheduleId, DateTime notificationTime);
        bool DeleteNotificationSchedule(int eventScheduleId);
        bool DeleteNotificationInfinity(int eventId);
    }
}