namespace Business.Services.User
{
    using Data.Repository.Interfaces;
    using static Business.Mapper;
    using Models;
    public class UserService : IUserService
    {
        private IUser userRepos;

        public UserService(IUser userRepository)
        {
            userRepos = userRepository;
        }

        public void CreateUser(User user)
        {
            userRepos.CreateUser(Map.Map<User, Data.Models.User>(user));
        }

        public User GetUserByEmail(string email)
        {
            return Map.Map<Data.Models.User, User>(userRepos.GetUserByEmail(email));
        }

        public User GetUserByIdentityId(string id)
        {            
            return Map.Map<Data.Models.User, User>(userRepos.GetUserByIdentityId(id));
        }
    }
}