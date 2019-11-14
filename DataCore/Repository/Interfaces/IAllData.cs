using System;
using System.Collections.Generic;
using DataCore.Models;

namespace DataCore.Repository.Interfaces
{
    public interface IAllData : IRepository<AllData>
    {
        IEnumerable<AllData> GetDataEvents(User @user, IEnumerable<Calendar> @calendarsList, DateTime dateTimeStart, DateTime dateTimeFinish);
        AllData GetEvent(int eventId);
    }
}