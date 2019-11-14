namespace Business.Services.Calendar
{
    using System;
    using Models;
    using System.Collections.Generic;
    using Data.Repository;
    using Data.Repository.Interfaces;
    public class CalendarService : ICalendarService
    {
        public IEnumerable<Calendar> GetCalendars(string session)
        {
            throw new NotImplementedException();
        }

        public Calendar GetCalendar(string session, int calendarId)
        {
            throw new NotImplementedException();
        }

        public int AddCalendar(string session, Calendar calendar)
        {
            throw new NotImplementedException();
        }
    }
}