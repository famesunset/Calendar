﻿namespace Business.Services.Calendar
{
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;
    using static Mapper;

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
                var userCalendars = calendarRepos.GetUserCalendars(dataUser.IdUser);
                var calendars = userCalendars
                    .Select(c => Map.Map<Data.Models.Calendar, Calendar>(c))
                    .OrderBy(c => c.Id);

                foreach (var calendar in calendars)
                {
                    calendar.IsOwner = calendar.OwnerId.Equals(dataUser.IdUser);
                }
                return calendars;
            }
            return new List<Calendar>();
        }

        public Calendar GetCalendar(string loginedUserId, int calendarId)
        {
            var dataCalendar = serviceHelper.IsUserHasAccessToCalendar(loginedUserId, calendarId);
            var dataUser = serviceHelper.GetUserByIdentityId(loginedUserId);
            if (dataCalendar != null)
            {
                var calendar = Map.Map<Data.Models.Calendar, Calendar>(dataCalendar);
                calendar.IsOwner = calendar.OwnerId.Equals(dataUser.IdUser);
                return calendar;
            }
            return null;
        }

        public int CreateCalendar(string loginedUserId, string name, int colorId, Access access)
        {
            var color = colorRepos.GetColorById(colorId);
            var calendar = new Calendar()
            {

                Name = name,
                Color = Map.Map<Data.Models.Color, Color>(color),
                Access = access
            };


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
            int? deleted = calendarRepos.RemoveCalendar(dataCalendar.Id);
            return dataCalendar != null && deleted == null;
        }

        public IEnumerable<Color> GetCalendarColors()
        {
            return colorRepos.GetColors().Select(c => Map.Map<Data.Models.Color, Color>(c));
        }

        public Color GetCalendarColor(int id)
        {
            return Map.Map<Data.Models.Color, Color>(colorRepos.GetColorById(id));
        }

        public void SubscribeUser(int userId, int calendarId)
        {
            calendarRepos.SubscribeUserToCalendar(userId, calendarId);
        }

        public void UnsubscribeUser(int userId, int calendarId)
        {
            calendarRepos.UnsubscribeUserFromCalendar(userId, calendarId);
        }
    }
}