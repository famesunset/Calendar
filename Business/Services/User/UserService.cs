namespace Business.Services.User
{
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;
    using static Business.AMapper;

    public class UserService : IUserService
    {
        private IUser userRepos;
        private ServiceHelper serviceHelper;

        public UserService(IUser userRepository, ICalendar calendarRepository, IAllData bigDataRepository)
        {
            userRepos = userRepository;
            serviceHelper = new ServiceHelper(userRepository, calendarRepository, bigDataRepository);
        }

        public bool AddBrowser(string identityId, string browser)
        {
            var dataUser = serviceHelper.GetUserByIdentityId(identityId);
            if (dataUser != null && browser != null)
            {
                userRepos.AddBrowser(Mapper.Map<Browser, Data.Models.Browser>
                (
                    new Browser
                    {
                        UserId = dataUser.IdUser,
                        BrowserId = browser
                    }
                ));
                return true;
            }
            return false;
        }

        public void CreateUser(User user)
        {
            userRepos.CreateUser(Mapper.Map<User, Data.Models.User>(user));
        }

        public IEnumerable<Browser> GetBrowsers(int calendarId)
        {
            return userRepos.GetBrowsers(calendarId).Select(b => Mapper.Map<Data.Models.Browser, Browser>(b));
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