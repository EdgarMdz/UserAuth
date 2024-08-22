using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Models;
using UserAuth.Services;

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService _userService, ILogger<AuthController> _logger)
        : ControllerBase
    {

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO user)
        {
            if (_userService == null)
            {
                _logger.LogError("UserService is not available. Time: {Time}", DateTime.UtcNow);
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(user.UserName?.Trim()))
            {
                _logger.LogWarning(
                    "Invalid username provided. UserName: {UserName}, Time:{Time}",
                    user.UserName,
                    DateTime.UtcNow
                );

                return BadRequest("User name not valid");
            }

            user.UserName = user.UserName.Trim();

            if (string.IsNullOrEmpty(user.Password))
            {
                _logger.LogWarning("Empty password provided");
                return BadRequest("Enter a password");
            }

            if (_userService.FindUser(user.UserName) != null)
            {
                _logger.LogWarning(
                    "Username already taken. UserName: {UserName}, Time: {Time}",
                    user.UserName,
                    DateTime.UtcNow
                );
                return BadRequest("The username is already taken.");
            }

            try
            {
                var createdUser = _userService.CreateUser(user, Role.General);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An error occuerred while creating the user. User:{User}, Time: {Time}",
                    user,
                    DateTime.UtcNow
                );

                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO user)
        {
            if (_userService == null)
            {
                _logger.LogError("UserService is not available. Time:{Time}", DateTime.UtcNow);
                return NotFound();
            }

            if (string.IsNullOrEmpty(user.UserName?.Trim()))
            {
                _logger.LogWarning(
                    "Invalid username provided for login.Time:{Time}",
                    DateTime.UtcNow
                );
                return BadRequest("Enter a valid user name");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                _logger.LogWarning(
                    "Invalid password provided for login. Time:{Time}",
                    DateTime.UtcNow
                );
                return BadRequest("Enter the password");
            }

            if (_userService.FindUser(user.UserName) == null)
            {
                _logger.LogWarning(
                    "Login attempt with non-existing username. UserName:{UserName},Time:{Time}",
                    user.UserName,
                    DateTime.UtcNow
                );
                return BadRequest("User or password is incorrect");
            }

            string token;
            try
            {
                token = _userService.LogIn(user);
            }
            catch (InvalidOperationException exception)
            {
                _logger.LogError(
                    exception,
                    "Failed login attempt. UserName:{UserName}, Time:{Time}",
                    user.UserName,
                    DateTime.UtcNow
                );

                return BadRequest("Password or user name are not valid");
            }

            return token;
        }

        [HttpGet("hi"), Authorize]
        public ActionResult<string> Hi()
        {
            return Ok("hello");
        }
    }
}
