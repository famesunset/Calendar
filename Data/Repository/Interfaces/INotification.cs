using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface INotification
    {
        IEnumerable<Notification> CreateNotification(Notification notification);
        IEnumerable<NotificationSchedule> CreateScheduleNotification(int eventScheduleId, DateTime notificationTime);
    }
}