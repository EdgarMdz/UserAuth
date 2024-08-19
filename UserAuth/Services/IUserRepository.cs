using DB;

namespace UserAuth.Services
{
    public interface IUserRepository
    {
        public User? GetUserByName(string username);
        public void AddUser(User user);
    }
}
