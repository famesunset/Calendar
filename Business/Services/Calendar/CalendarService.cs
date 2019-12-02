namespace Business.Services.Calendar
{
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;
    using static AMapper;

    public class CalendarService : ICalendarService
    {
        private readonly ServiceHelper serviceHelper;

        private readonly ICalendar calendarRepos;
        private readonly IColor colorRepos;
        public CalendarService(ICalendar calendarRepository, IUser userRepository, IAllData bigEventRepository, IColor colorRepository)
        {
            calendarRepos = calendarRepository;

            serviceHelper = new ServiceHelper(userRepository, calendarRepository, bigEventRepository);
            colorRepos = colorRepository;
        }
        public IEnumerable<Calendar> GetCalendars(string loginedUserId)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var userCalendars = serviceHelper.WrapMethodWithReturn(() => calendarRepos.GetUserCalendars(dataUser.IdUser),
                    new List<Data.Models.Calendar>());
                var calendars = userCalendars
                    .Select(c => Mapper.Map<Data.Models.Calendar, Calendar>(c))
                    .OrderBy(c => c.Id)
                    .ToList();

                for (var i = 0; i < calendars.Count(); ++i)
                {
                    var calendar = calendars[i];
                    var (isOwner, isDefault) = serviceHelper.GetCalendarData(calendar, dataUser);
                    calendar.IsOwner = isOwner;
                    calendar.IsDefault = isDefault;
                }

                return calendars;
            }
            return new List<Calendar>();
        }

        public Calendar GetCalendar(string loginedUserId, int calendarId)
        {
            var (dataUser, dataCalendar) = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, calendarId);
            if (dataCalendar != null)
            {
                var calendar = Mapper.Map<Data.Models.Calendar, Calendar>(dataCalendar);
                var (isOwner, isDefault) = serviceHelper.GetCalendarData(calendar, dataUser);
                calendar.IsOwner = isOwner;
                calendar.IsDefault = isDefault;
                return calendar;
            }
            return null;
        }

        public int CreateCalendar(string loginedUserId, string name, int colorId, Access access)
        {
            var color = serviceHelper.WrapMethodWithReturn(() => colorRepos.GetColorById(colorId), null);
            var calendar = new Calendar
            {
                Name = name,
                Color = color == null ? null : Mapper.Map<Data.Models.Color, Color>(color),
                Access = access
            };


            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataUser != null)
            {
                var dCalendar = Mapper.Map<Calendar, Data.Models.Calendar>(calendar);
                var calendarId = serviceHelper.WrapMethodWithReturn(() => calendarRepos.CreateCalendar(dataUser.IdUser, dCalendar), -1);
                return calendarId;
            }
            return -1;
        }

        public bool DeleteCalendar(string loginedUserId, int calendaId)
        {
            var (dataUser, dataCalendar) = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, calendaId);
            if (dataCalendar != null)
            {
                return serviceHelper.WrapMethodWithReturn(() => calendarRepos.RemoveCalendar(dataCalendar.Id), false);
            }

            return false;
        }

        public IEnumerable<Color> GetCalendarColors()
        {
            return colorRepos.GetColors()?.Select(c => Mapper.Map<Data.Models.Color, Color>(c));
        }

        public Color GetCalendarColor(int id)
        {
            var colors = serviceHelper.WrapMethodWithReturn(() => colorRepos.GetColorById(id), null);
            if (colors != null)
            {
                return Mapper.Map<Data.Models.Color, Color>(colors);
            }

            return null;
        }

        public bool SubscribeUser(int userId, int calendarId)
        {
            var userCaledars = serviceHelper.WrapMethodWithReturn(() => calendarRepos.GetUserCalendars(userId), null);

            if (userCaledars != null && !userCaledars.Any(c => c.Id.Equals(calendarId)))
            {
                var success = serviceHelper.WrapMethod(() => calendarRepos.SubscribeUserToCalendar(userId, calendarId));
                return success;
            }
            return false;
        }

        public bool UnsubscribeUser(string loginedUserId, int calendarId)
        {
            var user = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (user != null)
            {
                var success = serviceHelper.WrapMethod(() => calendarRepos.UnsubscribeUserFromCalendar(user.IdUser, calendarId));
                return success;
            }

            return false;
        }
    }
}