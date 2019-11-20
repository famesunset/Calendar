namespace Business.Tests.FakeRepositories
{
    using System;
    using Data.Models;
    using Data.Repository.Interfaces;
    
    public class FakeUserRepository : IUser
    {
        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByIdentityId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
