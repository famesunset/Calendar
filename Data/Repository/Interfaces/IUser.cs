using System.Collections.Generic;
using Data.Models;

namespace Data.Repository.Interfaces
{
    public interface IUser : IRepository<User>
    {
        void CreateUser(User user);
        User GetUserByIdentityId(string id);
    }
}