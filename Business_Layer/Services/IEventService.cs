using System;
using System.Collections.Generic;
using Business_Layer.Models;

namespace Business_Layer.Services
{
    public interface IEventService
    {
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
        /// <returns>Событие</returns>
        Event GetEvent(string session, int id);
        /// <summary>
        /// Получить список событий за определённый период времени
        /// </summary>
        /// <param name="session">Сессия пользователя</param>
        /// <param name="calendarIds">Список календарей</param>
        /// <param name="dateUnit">Какой тип календаря открыт</param>
        /// <param name="beginning">День, с которого начинается открытый календарь</param>
        /// <returns>Список событий</returns>
        IEnumerable<Event> GetEvents(string session, ICollection<int> calendarIds, DateUnit dateUnit, DateTime beginning);
    }
}