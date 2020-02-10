using System;
using Xunit;
using SportsBetsServer.Services;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsAPI.UnitTests.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        public AuthServiceTest()
        {
           // _authService = new AuthService();
        }
        [Fact]
        public void CreatePasswordHashTest()
        {
            byte[] passHash;
            byte[] passSalt;

            _authService.CreatePasswordHash("password", out passHash, out passSalt);

            // Assert.NotNull(passHash);
            // Assert.NotNull(passSalt);
        }
    }
}
