namespace Business.Services
{
    using System;
    using System.Linq;
    using Data.Repository.Interfaces;

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
                var dataUser = WrapMethodWithReturn(() => userRepos.GetUserByIdentityId(loginedUserId), null);
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
                var dataCalendars = WrapMethodWithReturn(() => calendarRepos.GetUserCalendars(dataUser.IdUser), null);
                if (dataCalendars != null)
                {
                    return dataCalendars.SingleOrDefault(c => c.Id == calendarId);
                }
            }
            return null;
        }

        public Data.Models.AllData IsUserHasAccessToEvent(string loginedUserId, int eventId)
        {
            var dataUser = GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var dataCalendars = WrapMethodWithReturn(() => calendarRepos.GetUserCalendars(dataUser.IdUser), null);
                var dataBigEvent = WrapMethodWithReturn(() => bigEventRepos.GetEvent(eventId), null);
                if (dataBigEvent != null && dataCalendars != null)
                {
                    if (dataCalendars.Any(c => c.Id.Equals(dataBigEvent.CalendarId)))
                    {
                        return dataBigEvent;
                    }
                }
            }
            return null;
        }

        public (bool IsOwner, bool IsDefault) GetCalendarData(Models.Calendar calendar, Data.Models.User dataUser)
        {
            var isOwner = calendar.OwnerId.Equals(dataUser.IdUser);
            var isDefault = WrapMethodWithReturn(() => calendarRepos.CheckDefaultCalendar(calendar.Id), false);
            return (isOwner, isDefault);
        }

        public T WrapMethodWithReturn<T>(Func<T> function, T defaultReturn)
        {
            try
            {
                return function();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = default;
                return defaultReturn;
            }
        }

        public bool WrapMethod(Action function)
        {
            try
            {
                function();
                return true;
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = default;
                return false;
            }
        }
    }
}
