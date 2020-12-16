using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using LoggerService;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthService _authService;
        private readonly IAccountRepository _repo;

        public AuthController(
            ILoggerManager logger, 
            IAuthService authService,
            IAccountRepository repo)
        {
            _logger = logger;
            _authService = authService;
            _repo = repo;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody]UserCredentials userToLogin)
        {

            var account = _repo.GetUserByUsername(userToLogin.Username);

            if (account == null || (!_authService.VerifyPassword(userToLogin.Password, account.HashedPassword)))
            {
                return BadRequest("Incorrect Username and/or Password.");
            }

            var user = new User
            {
                Id = account.Id,
                Username = account.Username,
                Role = account.Role
            };

            var signedAndEncodedToken = _authService.CreateJsonToken(user);

            _logger.LogInfo($"Token successfully created. Encoded as {signedAndEncodedToken}");
            return Ok(signedAndEncodedToken);
        }
    }
    
}