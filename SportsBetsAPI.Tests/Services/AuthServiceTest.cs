using System;
using Xunit;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsAPI.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;

        public AuthServiceTest()
        {

        }
        [Fact]
        public void CreatePasswordHashTest()
        {
            _authService.CreatePasswordHash("password", out byte[] passHash, out byte[] passSalt);

            Assert.NotNull(passHash);
            Assert.NotNull(passSalt);
        }
    }
}
