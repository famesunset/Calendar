namespace Business.Services.User
{
    using Models;
    using Data.Repository.Interfaces;
    
    using static Business.AMapper;

    public class UserService : IUserService
    {
        private IUser userRepos;

        public UserService(IUser userRepository)
        {
            userRepos = userRepository;
        }

        public void CreateUser(User user)
        {
            userRepos.CreateUser(Mapper.Map<User, Data.Models.User>(user));
        }

        public User GetUserByEmail(string email)
        {
            return Mapper.Map<Data.Models.User, User>(userRepos.GetUserByEmail(email));
        }

        public User GetUserByIdentityId(string id)
        {            
            return Mapper.Map<Data.Models.User, User>(userRepos.GetUserByIdentityId(id));
        }
    }
}