using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using LoggerService;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthService _authService;
        private readonly IRepositoryBase<User> _userRepo;

        public AuthController(
            ILoggerManager logger, 
            IAuthService authService,
            IRepositoryBase<User> repo)
        {
            _logger = logger;
            _authService = authService;
            _userRepo = repo;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody]UserCredentials userToLogin)
        {

            var user = _userRepo.GetUserByUsername(userToLogin.Username);

            if (user == null || (!_authService.VerifyPassword(userToLogin.Password, user.HashedPassword)))
            {
                return BadRequest("Incorrect Username and/or Password.");
            }

            var signedAndEncodedToken = _authService.CreateJsonToken(user);

            _logger.LogInfo($"Token successfully created. Encoded as {signedAndEncodedToken}");
            return Ok(signedAndEncodedToken);
        }
    }
    
}