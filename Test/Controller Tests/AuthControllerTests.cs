using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using UserAuth.Controllers;
using UserAuth.Models;
using UserAuth.Services;

namespace Test.Controller_Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserService> _mockUserService;
        private readonly AuthController _controller;
        private readonly UserDTO userdto;
        private readonly User user;

        public AuthControllerTests()
        {
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserService = new Mock<IUserService>();
            _controller = new AuthController(_mockUserService.Object, _mockLogger.Object);
            userdto = new() { Password = "123", UserName = "username" };
            user = new() { UserName = userdto.UserName };
        }

        [Fact]
        public void Register_ValidUser_ReturnsOkResultWithUser()
        {
            // Arrange

            _mockUserService.Setup(s => s.FindUser(It.IsAny<string>())).Returns(null as UserDTO);

            _mockUserService
                .Setup(s => s.CreateUser(It.IsAny<UserDTO>(), It.IsAny<Role>()))
                .Returns(user);

            //Act
            var result = _controller.Register(userdto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user.UserName, returnedUser.UserName);
        }

        [Fact]
        public void Register_IUserServiceIsNull_ReturnsNotFoundResult()
        {
            // Arrage
            var controller = new AuthController(null, _mockLogger.Object);

            // Act
            var result = controller.Register(userdto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
