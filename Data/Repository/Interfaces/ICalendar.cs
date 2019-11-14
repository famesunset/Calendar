using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface ICalendar : IRepository<Calendar>
    {
        IEnumerable<Calendar> AddCalendar(User @user, Calendar @calendar);
    }
}