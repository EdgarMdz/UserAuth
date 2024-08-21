using DB;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuth.Models;

namespace UserAuth.Services
{
    public class UserService(IUserRepository _userRepository, IConfiguration _configuration) : IUserService
    {
        public User CreateUser(UserDTO userDTO, Role role)
        {
            using var hmac = new HMACSHA512();

            User user = new()
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                PasswordSalt = hmac.Key,
                Role = role,
                UserName = userDTO.UserName
            };

            _userRepository.AddUser(user);

            return _userRepository.GetUserByName(user.UserName) ?? throw new ArgumentNullException("Could not create the user");
        }

        public UserDTO? FindUser(string userName)
        {
            var user = _userRepository.GetUserByName(userName);

            return user != null ? new UserDTO { UserName = user.UserName } : null;

        }

        public string LogIn(UserDTO userDto)
        {
            var user = _userRepository.GetUserByName(userDto.UserName);

            if (!VerifyPassword(userDto.Password, user.PasswordSalt, user.PasswordHash))
                throw new InvalidOperationException("Wrong password");

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>() ??
                throw new InvalidProgramException("No field for Jwt in appsettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Role, user.Role.ToString())]),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = cred,
                Issuer = jwt.Issuer,
                Audience = jwt.Audience
            };

            var toke = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(toke);
        }

        private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
