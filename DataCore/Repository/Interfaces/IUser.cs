using System.Collections.Generic;

namespace DataCore.Repository.Interfaces
{
    public interface IUser : IRepository<User>
    {
        IEnumerable<User> CreateUser(string name, string mobile, string email, int idIdentity);
    }
}