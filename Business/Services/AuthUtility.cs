namespace Business.Services.Event
{
    using System.Linq;

    public partial class EventService
    {
        private Data.Models.User IsUserLoggined(string loginedUserId)
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

        private bool IsUserHasAccessToCalendar(string loginedUserId, int calendarId)
        {
            var dataUser = IsUserLoggined(loginedUserId);
            if (dataUser != null)
            {
                var dataCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                return dataCalendars.Any(c => c.Id.Equals(calendarId));
            }
            return false;
        }

        private Data.Models.AllData IsUserHasAccessToEvent(string loginedUserId, int eventId)
        {
            var dataUser = IsUserLoggined(loginedUserId);
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
