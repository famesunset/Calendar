namespace Business.Services.Calendar
{
    using System.Collections.Generic;
    using Models;
    public interface ICalendarService
    {
        IEnumerable<Calendar> GetCalendars(string loginedUserId);
        Calendar GetCalendar(string loginedUserId, int calendarId);
        int CreateCalendar(string loginedUserId, Calendar calendar);
        bool DeleteCalendar(string loginedUserId, int calendaId);
    }
}