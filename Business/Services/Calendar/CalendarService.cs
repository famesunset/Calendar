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
        public IEnumerable<Calendar> GetCalendars(string session)
        {
            ICalendar calendarRepository = new CalendarRepo();
            // var userRepo = new UserRepo();
            var userId = 1; // userRepo.GetUser(session);
            var userCalendars = calendarRepository.GetUserCalendars(userId);
            return userCalendars.Select(c => Map.Map<Data.Models.Calendar, Calendar>(c));
        }

        public Calendar GetCalendar(string session, int calendarId)
        {
            ICalendar calendarRepository = new CalendarRepo();
            // var userRepo = new UserRepo();
            var userId = 1; // userRepo.GetUser(session);
            var calendar = calendarRepository.GetCalendarById(calendarId);
            var userCalendars = calendarRepository.GetUserCalendars(userId);
            if (userCalendars.Any(c => c.Id.Equals(calendar.Id)))
            {
                var bCalendar = Map.Map<Data.Models.Calendar, Calendar>(calendar);
                return bCalendar;
            }
            return null;
        }

        public int CreateCalendar(string session, Calendar calendar)
        {
            ICalendar calendarRepository = new CalendarRepo();
            // var userRepo = new UserRepo();
            var userId = 1; // userRepo.GetUser(session);
            var dCalendar = Map.Map<Calendar, Data.Models.Calendar>(calendar);
            var calendarId = calendarRepository.CreateCalendar(userId, dCalendar);
            return calendarId;
        }

        public void RemoveCalendar(User user, Calendar calendar)
        {
            if (user.Id == calendar.UserOwnerId)
            {
                ICalendar calendarRepository = new CalendarRepo();
                calendarRepository.RemoveCalendar(calendar.Id);
            }
        }
    }
}