using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Models;

namespace Data_Layer.Repository.Interfaces
{
    interface IAllData : IRepository<AllData>
    {
        IEnumerable<AllData> GetDataEvents(int userId, List<int> idsLCalendars, DateTime dateTimeStart, DateTime dateTimeFinish);
    }
}
