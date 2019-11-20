namespace Business.Tests.FakeRepositories
{
  using System;
  using System.Collections.Generic;
  using Data.Models;
  using Data.Repository.Interfaces;
  
  public class FakeCalendarRepository : ICalendar
  {
    public int CreateCalendar(int userId, Calendar calendar)
    {
      throw new NotImplementedException();
    }

    public Calendar GetCalendarById(int calendarId)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Calendar> GetUserCalendars(int userId)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<User> GetUsersByCalendarId(int calendarId)
    {
      throw new NotImplementedException();
    }

    public int? RemoveCalendar(int id)
    {
      throw new NotImplementedException();
    }
  }
}