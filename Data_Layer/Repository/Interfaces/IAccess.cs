using System.Collections.Generic;

namespace Data_Layer.Repository.Interfaces
{
    public interface IAccess : IRepository<Access>
    {
        IEnumerable<Access> GetNameById(int id);
    }
}
