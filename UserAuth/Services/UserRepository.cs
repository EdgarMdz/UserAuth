using DB;

namespace UserAuth.Services
{
    public class UserRepository(UserContext usersContext) : IUserRepository
    {
        public void AddUser(User user)
        {
            usersContext.Users.Add(user);
            usersContext.SaveChanges();
        }

        public User? GetUserByName(string username) =>
            usersContext.Users.Where(u => u.UserName.ToLower() == username).FirstOrDefault();

    }
}
