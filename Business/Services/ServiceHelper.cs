namespace Business.Services
{
    using Data.Repository;
    using Data.Repository.Interfaces;
    using System.Linq;

    public class ServiceHelper
    {
        private readonly IEvent eventRepos;
        private readonly IUser userRepos;
        private readonly ICalendar calendarRepos;
        private readonly IAccess accessRepos;
        private readonly INotification notificationRepos;
        private readonly IAllData bigEventRepos;

        public ServiceHelper()
        {
            eventRepos = new EventRepo();
            userRepos = new UserRepo();
            calendarRepos = new CalendarRepo();
            accessRepos = new AccessRepo();
            notificationRepos = new NotificationRepo();
            bigEventRepos = new AllDataRepo();
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
                return dataCalendars.SingleOrDefault(c => c.Id.Equals(calendarId));
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
                if (dataCalendars.Any(c => c.Id.Equals(dataBigEvent.IdCalendar)))
                {
                    return dataBigEvent;
                }
            }
            return null;
        }
    }
}
