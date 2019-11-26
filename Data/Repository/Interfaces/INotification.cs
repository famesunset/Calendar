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
        IEnumerable<Notification> UpdateNotification(int eventId, int minutesBefore);
        IEnumerable<Notification> UpdateNotificationInfinity(int eventId, int minutesBefore);
        IEnumerable<Notification> UpdateNotificationSchedule(int @eventScheduleId, DateTime @notificationTime);
    }
}