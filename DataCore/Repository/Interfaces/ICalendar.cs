using System.Collections.Generic;

namespace DataCore.Repository.Interfaces
{
    public interface ICalendar : IRepository<Calendar>
    {
        IEnumerable<Calendar> AddCalendar(User @user, Calendar @calendar);
    }
}