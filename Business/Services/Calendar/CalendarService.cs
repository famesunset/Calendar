using System.Linq;

namespace Business.Services.Calendar
{
    using Models;
    using System.Collections.Generic;
    using Data.Repository;
    using Data.Repository.Interfaces;
    using static Mapper;

    public class CalendarService : ICalendarService
    {
        private readonly ServiceHelper serviceHelper;
        private readonly ICalendar calendarRepos;
        public CalendarService()
        {
            serviceHelper = new ServiceHelper();
            calendarRepos = new CalendarRepo();
        }
        public IEnumerable<Calendar> GetCalendars(string loginedUserId)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var userCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                return userCalendars.Select(c => Map.Map<Data.Models.Calendar, Calendar>(c));
            }
            return null;
        }

        public Calendar GetCalendar(string loginedUserId, int calendarId)
        {
            var dataCalendar = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, calendarId);
            if (dataCalendar != null)
            {
                return Map.Map<Data.Models.Calendar, Calendar>(dataCalendar);
            }
            return null;
        }

        public int CreateCalendar(string loginedUserId, Calendar calendar)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if(dataUser != null)
            {
                var dCalendar = Map.Map<Calendar, Data.Models.Calendar>(calendar);
                var calendarId = calendarRepos.CreateCalendar(dataUser.IdUser, dCalendar);
                return calendarId;
            }
            return -1;
        }

        public bool DeleteCalendar(string loginedUserId, int calendaId)
        {
            var dataCalendar = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, calendaId);
            if(dataCalendar != null)
            {
                calendarRepos.DeleteCalendar(dataCalendar.Id);
                return true;
            }
            return false;
        }
    }
}