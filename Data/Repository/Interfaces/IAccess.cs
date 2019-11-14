using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IAccess : IRepository<Access>
    {
        IEnumerable<Access> GetNameById(Access @access);
    }
}