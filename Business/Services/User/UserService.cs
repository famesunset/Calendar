namespace Business.Services.User
{
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Repository.Interfaces;
    using static AMapper;

    public class UserService : IUserService
    {
        private readonly IUser userRepos;
        private readonly ServiceHelper serviceHelper;

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
                var success = serviceHelper.WrapMethod(() => userRepos.AddBrowser(Mapper.Map<Browser, Data.Models.Browser>
                (
                    new Browser
                    {
                        UserId = dataUser.IdUser,
                        BrowserId = browser
                    }
                )));
                return success;
            }
            return false;
        }

        public bool CreateUser(User user)
        {
            var success = serviceHelper.WrapMethod(() =>userRepos.CreateUser(Mapper.Map<User, Data.Models.User>(user)));
            return success;
        }

        public IEnumerable<Browser> GetBrowsers(int calendarId)
        {
            var browsers = serviceHelper.WrapMethodWithReturn(() => userRepos.GetBrowsers(calendarId), null);
            return browsers?.Select(b => Mapper.Map<Data.Models.Browser, Browser>(b));
        }

        public bool RemoveBrowser(string browser)
        {
            var result = serviceHelper.WrapMethod(() => userRepos.RemoveBrowser(browser));
            return result;
        }

        public User GetUserByEmail(string email)
        {
            var user = serviceHelper.WrapMethodWithReturn(() => userRepos.GetUserByEmail(email), null);
            if (user != null)
            {
                return Mapper.Map<Data.Models.User, User>(user);
            }

            return null;
        }

        public User GetUserByIdentityId(string identityId)
        {
            var user = serviceHelper.WrapMethodWithReturn(() => userRepos.GetUserByIdentityId(identityId), null);
            if (user != null)
            {
                return Mapper.Map<Data.Models.User, User>(user);
            }

            return null;
        }
    }
}