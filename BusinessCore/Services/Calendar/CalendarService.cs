namespace BusinessCore.Services.Calendar
{
    using System;
    using Models;
    using System.Collections.Generic;
    using DataCore.Repository;
    using DataCore.Repository.Interfaces;
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
            ICalendar calendarRepos = new CalendarRepo();
            //int userId = 1;
            //calendarRepos.AddCalendar(calendar.Name, (int)calendar.Access/*, userId*/);
            return -1;
        }
    }
}