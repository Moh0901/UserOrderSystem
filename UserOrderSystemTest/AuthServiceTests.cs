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
    public class AuthServiceTests
    {
        [Fact]
        public async Task IsEmailRegisteredAsync_ReturnsTrue_WhenExists()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetUserByEmailAsync("user@example.com"))
                    .ReturnsAsync(new User { Email = "user@example.com" });

            var service = new AuthService(mockRepo.Object);
            var result = await service.IsEmailRegisteredAsync("user@example.com");

            Assert.True(result);
        }

        [Fact]
        public async Task IsEmailRegisteredAsync_ReturnsFalse_WhenNotExists()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetUserByEmailAsync("new@example.com")).ReturnsAsync((User?)null);

            var service = new AuthService(mockRepo.Object);
            var result = await service.IsEmailRegisteredAsync("new@example.com");

            Assert.False(result);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void IsValidPassword_ReturnsFalse_ForNullOrEmpty(string? password, bool expected)
        {
            var service = new AuthService(null!);
            var result = service.IsValidPassword(password ?? "");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Pass1234", true)]
        [InlineData("short1", false)]
        [InlineData("nouppercase1", false)]
        [InlineData("NOLOWERCASE1", true)]
        [InlineData("NoNumber", false)]
        public void IsValidPassword_ChecksVariousInputs(string password, bool expected)
        {
            var service = new AuthService(null!);
            var result = service.IsValidPassword(password);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task IsEmailRegisteredAsync_IgnoresCase_WhenCheckingEmail()
        {
            var mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(r => r.GetUserByEmailAsync("user@example.com"))
            //        .ReturnsAsync(new User { Email = "user@example.com" });

            mockRepo.Setup(r => r.GetUserByEmailAsync(It.Is<string>(s => s.Equals("user@example.com", StringComparison.OrdinalIgnoreCase)))).ReturnsAsync(new User { Email = "user@example.com" });

            var service = new AuthService(mockRepo.Object);

            var result = await service.IsEmailRegisteredAsync("USER@EXAMPLE.COM");

            Assert.True(result);
        }
        [Fact]
        public async Task IsEmailRegisteredAsync_Throws_WhenRepositoryFails()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetUserByEmailAsync(It.IsAny<string>()))
                    .ThrowsAsync(new Exception("DB failure"));

            var service = new AuthService(mockRepo.Object);

            await Assert.ThrowsAsync<Exception>(() => service.IsEmailRegisteredAsync("user@example.com"));
        }
    }
}
