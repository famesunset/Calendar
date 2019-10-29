using System;
using System.Collections.Generic;
using Data_Layer.Models;

namespace Data_Layer.Repository.Interfaces
{
    public interface IAllData : IRepository<AllData>
    {
        IEnumerable<AllData> GetDataEvents(int userId, List<int> idsLCalendars, DateTime dateTimeStart, DateTime dateTimeFinish);
    }
}
