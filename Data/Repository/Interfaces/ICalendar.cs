using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface ICalendar : IRepository<Calendar>
    {
        int CreateCalendar(int userId, Calendar @calendar);
        Calendar GetCalendarById(int calendarId);
        IEnumerable<Calendar> GetUserCalendars(int userId);
        IEnumerable<User> GetUsersByCalendarId(int calendarId);
        int? RemoveCalendar(int id);
    }
}