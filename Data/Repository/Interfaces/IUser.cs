using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IUser : IRepository<User>
    {
        IEnumerable<User> CreateUser(string name, string mobile, string email, int idIdentity);
    }
}