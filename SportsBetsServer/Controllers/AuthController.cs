using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
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
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(
            ILoggerManager logger, 
            IAuthService authService,
            IUserService userService,
            IConfiguration config)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
            _config = config;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(500)] 
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody]UserCredentials userToLogin)
        {            
            try
            {
                var user = await _userService.GetUserByUsernameAsync(userToLogin.Username);

                if (!_authService.VerifyPassword(userToLogin.Password, user.HashedPassword))
                {
                    return BadRequest();
                }

                var signedAndEncodedToken = _authService.CreateJsonToken(_config, user);

                _logger.LogInfo($"Token successfully created. Encoded as {signedAndEncodedToken}");
                return Ok(signedAndEncodedToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login error {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
    
}