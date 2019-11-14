using System.Collections.Generic;

namespace DataCore.Repository.Interfaces
{
    public interface IAccess : IRepository<Access>
    {
        IEnumerable<Access> GetNameById(Access @access);
    }
}