using Data.Repository;
using static Business.Mapper;

namespace Business.Services.User
{
    using Models;
    public class UserService : IUserService
    {
        private UserRepo userRepos;

        public UserService()
        {
            userRepos = new UserRepo();
        }

        public void CreateUser(User user)
        {
            userRepos.CreateUser(Map.Map<User, Data.Models.User>(user));
        }

        public User GetUserByIdentityId(string id)
        {            
            return Map.Map<Data.Models.User, User>(userRepos.GetUserByIdentityId(id));
        }
    }
}