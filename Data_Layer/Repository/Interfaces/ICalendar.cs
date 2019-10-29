using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface ICalendar : IRepository<Calendar>
    {
        IEnumerable<Calendar> AddCalendar(string name, int accessId);
    }
}