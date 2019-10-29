using System.Collections.Generic;
using Business_Layer.Models;

namespace Business_Layer.Services
{
    public interface ICalendarService
    {
        /// <summary>
        /// Получить список календарей
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <returns>Список календарей</returns>
        IEnumerable<Calendar> GetCalendars(string session);
        /// <summary>
        /// Получить конкретный календарь
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <param name="calendarId">Id календаря</param>
        /// <returns>Календарь</returns>
        Calendar GetCalendar(string session, int calendarId);
        /// <summary>
        /// Создать календарь
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <param name="calendar">Экземпляр календаря</param>
        /// <returns></returns>
        int AddCalendar(string session, Calendar calendar);
        
    }
}