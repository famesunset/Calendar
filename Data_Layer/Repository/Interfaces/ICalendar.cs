using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface ICalendar : IRepository<Calendar>
    {
        IEnumerable<Calendar> AddCalendar(User @user, Calendar @calendar);
    }
}