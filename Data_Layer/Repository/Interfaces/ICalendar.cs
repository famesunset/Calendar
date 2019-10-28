using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repository.Interfaces
{
    interface ICalendar : IRepository<Calendar>
    {
        IEnumerable<Calendar> AddCalendar(string name, int accessId);
    }
}