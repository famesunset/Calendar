using System.Collections.Generic;
using Data_Layer;
using Data_Layer.Repository;

namespace Repository
{
    public interface IAccess : IRepository<Access>
    {
        IEnumerable<Access> GetNameById(int id);
    }
}
