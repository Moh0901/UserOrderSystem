using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;
using UserOrderSystem.Services;

namespace UserOrderSystemTest
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserById_ReturnsUser()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(1)).Returns(new User { Id = 1, Email = "test@example.com" }); 

            var service = new UserService(mockRepo.Object);
            var result = service.GetUserById(1);

            Assert.NotNull(result);
            Assert.Equal("test@example.com", result!.Email);
        }
        [Fact]
        public void GetUserById_ReturnsNull_WhenUserNotFound()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(99)).Returns((User?)null);

            var service = new UserService(mockRepo.Object);
            var result = service.GetUserById(99);

            Assert.Null(result);
        }
        [Fact]
        public void GetUserById_ThrowsException_WhenRepositoryFails()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))
                    .Throws(new Exception("Database failure"));

            var service = new UserService(mockRepo.Object);

            Assert.Throws<Exception>(() => service.GetUserById(1));
        }
    }
}
