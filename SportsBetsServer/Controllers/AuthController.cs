using Microsoft.AspNetCore.Mvc;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities;
using SportsBetsServer.Repository;
using SportsBetsServer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using LoggerService;
using SportsBetsServer.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILoggerManager _logger;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public AuthController(
            ILoggerManager logger, 
            IAccountService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody]UserCredentials userToLogin)
        {

            var account = _service.GetAccountByUsername(userToLogin.Username);

            if (account == null || (!_service.VerifyPassword(userToLogin.Password, account.HashedPassword)))
            {
                throw new AppException("Incorrect Username and/or Password.");
            }

            var user = _mapper.Map<User>(account);

            var signedAndEncodedToken = _service.CreateJsonToken(user);
            SetTokenCookie(signedAndEncodedToken);
            var refreshToken = new RefreshToken();

            return Ok(user);
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            Response.Cookies.Append("token", token, cookieOptions);
        }
    }
    
}