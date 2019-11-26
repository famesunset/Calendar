namespace Business.Services.User
{
    using Models;
    public interface IUserService
    {
        void CreateUser(User user);
        User GetUserByIdentityId(string id);
        User GetUserByEmail(string email);
    }
}
