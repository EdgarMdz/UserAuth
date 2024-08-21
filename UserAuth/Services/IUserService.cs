using DB;
using UserAuth.Models;

namespace UserAuth.Services
{
    public interface IUserService
    {
        public User CreateUser(UserDTO userDTO, Role role);
        public UserDTO? FindUser(string userName);
        public string LogIn(UserDTO userDTO);
    }
}
