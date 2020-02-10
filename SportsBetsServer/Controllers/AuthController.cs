using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities.Extensions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using LoggerService;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(
            ILoggerManager logger, 
            IConfiguration config,
            IAuthService authService)
        {
            _logger = logger;
            _config = config;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserToRegister userToLogin)
        {
            var user = await _authService.LoginUserAsync(userToLogin.Username.ToLower(), userToLogin.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var signedAndEncodedToken = tokenHandler.WriteToken(token);

                _logger.LogInfo($"{token} successfully created. Encoded as {signedAndEncodedToken}");
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