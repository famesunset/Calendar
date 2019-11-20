using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IAccess
    {
        IEnumerable<Access> GetNameById(Access @access);
    }
}