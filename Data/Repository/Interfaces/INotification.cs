using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface INotification
    {
        void CreateNotification(int eventId, int before, int timeUnitId);
        void DeleteNotification(int eventId);
        void UpdateNotification(int eventId, int before, int timeUnitId);
    }
}