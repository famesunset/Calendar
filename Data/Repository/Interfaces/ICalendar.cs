using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface ICalendar
    {
        int CreateCalendar(int userId, Calendar @calendar);
        Calendar GetCalendarById(int calendarId);
        IEnumerable<Calendar> GetUserCalendars(int userId);
        IEnumerable<User> GetUsersByCalendarId(int calendarId);
        bool RemoveCalendar(int id);
        bool CheckDefaultCalendar(int @idCalendar);
        void SubscribeUserToCalendar(int userId, int calendarId);
        void UnsubscribeUserFromCalendar(int userId, int calendarId);
    }
}