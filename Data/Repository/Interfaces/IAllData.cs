using System;
using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IAllData : IRepository<AllData>
    {
        IEnumerable<AllData> GetDataEvents(int userId, IEnumerable<Calendar> @calendarsList, DateTime dateTimeStart, DateTime dateTimeFinish);
        AllData GetEvent(int eventId);
    }
}