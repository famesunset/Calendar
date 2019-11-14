namespace Business.Services.Calendar
{
    using System.Collections.Generic;
    using Models;
    public interface ICalendarService
    {
        IEnumerable<Calendar> GetCalendars(string session);
        Calendar GetCalendar(string session, int calendarId);
        int AddCalendar(string session, Calendar calendar);
        
    }
}