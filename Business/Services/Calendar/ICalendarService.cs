namespace Business.Services.Calendar
{
    using System.Collections.Generic;
    using Models;
    public interface ICalendarService
    {
        IEnumerable<Calendar> GetCalendars(string session);
        Calendar GetCalendar(string session, int calendarId);
        int CreateCalendar(string session, Calendar calendar);
        void RemoveCalendar(User user, Calendar calendar);
    }
}