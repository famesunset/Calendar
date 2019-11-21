namespace Business.Services
{
    using Data.Repository.Interfaces;
    using System.Linq;

    internal class ServiceHelper
    {
        private readonly IUser userRepos;
        private readonly ICalendar calendarRepos;
        private readonly IAllData bigEventRepos;

        public ServiceHelper(IUser userRepository, ICalendar calendarRepository, IAllData bigEventRepository)
        {
            userRepos = userRepository;
            calendarRepos = calendarRepository;
            bigEventRepos = bigEventRepository;
        }

        public Data.Models.User GetUserByIdentityId(string loginedUserId)
        {
            if (loginedUserId != null)
            {
                var dataUser = userRepos.GetUserByIdentityId(loginedUserId);
                if (dataUser != null && dataUser.IdUser > 0)
                {
                    return dataUser;
                }
            }
            return null;
        }

        public Data.Models.Calendar IsUserHasAccessToCalendar(string loginedUserId, int calendarId)
        {
            var dataUser = GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var dataCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                return dataCalendars.Where(c => c.Id == calendarId).FirstOrDefault();
            }
            return null;
        }

        public Data.Models.AllData IsUserHasAccessToEvent(string loginedUserId, int eventId)
        {
            var dataUser = GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var dataCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                var dataBigEvent = bigEventRepos.GetEvent(eventId);
                if (dataCalendars.Any(c => c.Id.Equals(dataBigEvent.CalendarId)))
                {
                    return dataBigEvent;
                }
            }
            return null;
        }
    }
}
