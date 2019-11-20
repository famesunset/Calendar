namespace Business.Tests.FakeRepositories
{
  using System;
  using System.Collections.Generic;
  using Data.Models;
  using Data.Repository.Interfaces;
  
  public class FakeAccessRepository : IAccess
  {
    public IEnumerable<Access> GetNameById(Access access)
    {
      throw new NotImplementedException();
    }
  }
}