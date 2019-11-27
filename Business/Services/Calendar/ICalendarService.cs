namespace Business.Services.Calendar
{
    using System.Collections.Generic;
    using Models;
    public interface ICalendarService
    {
        IEnumerable<Calendar> GetCalendars(string loginedUserId);
        Calendar GetCalendar(string loginedUserId, int calendarId);
        int CreateCalendar(string loginedUserId, string name, int colorId, Access access);
        bool DeleteCalendar(string loginedUserId, int calendaId);
        IEnumerable<Color> GetCalendarColors();
        Color GetCalendarColor(int id);
        void SubscribeUser(int userId, int calendarId);
        void UnsubscribeUser(string loginedUserId, int calendarId);
    }
}