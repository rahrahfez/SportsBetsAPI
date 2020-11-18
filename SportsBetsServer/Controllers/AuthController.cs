using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(500)] 
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody]UserCredentials userToLogin)
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
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.UserRole)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = credentials,
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"]
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