using System.Collections.Generic;
using Business_Layer.Models;

namespace Business_Layer
{
    public interface IService
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
        /// <summary>
        /// Создать событие
        /// </summary>
        /// <param name="session">Пока что кидай null. Сессия пользователя</param>
        /// <param name="event">Экземпляр события</param>
        /// <returns>Id нового события</returns>
        int AddEvent(string session, Event @event);
        /// <summary>
        /// Получить событие по id
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <param name="id">Id сессии</param>
        /// <returns>Id нового события</returns>
        Event GetEvent(string session, int id);
        /// <summary>
        /// Получить все события для конктреного календаря
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <param name="calendarId">Id календаря</param>
        /// <returns>Список событий</returns>
        IEnumerable<Event> GetEvents(string session, int calendarId);
    }
}