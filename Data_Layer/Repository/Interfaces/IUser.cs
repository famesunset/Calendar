using System.Collections.Generic;
using Data_Layer;
using Data_Layer.Repository;

namespace Repository.Interfaces
{
    public interface IUser : IRepository<User>
    {
        IEnumerable<User> CreateUser(string name, string mobile, string email, int idIdentity);
    }
}