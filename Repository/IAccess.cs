using System.Collections.Generic;
using Data_Layer;

namespace Repository
{
    public interface IAccess
    {
        IEnumerable<Access> GetNameById(Access access, int id);
    }
}
