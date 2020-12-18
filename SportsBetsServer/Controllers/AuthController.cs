using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using LoggerService;
using SportsBetsServer.Helpers;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILoggerManager _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repo;

        public AuthController(
            ILoggerManager logger, 
            IAccountService authService,
            IAccountRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _accountService = authService;
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody]UserCredentials userToLogin)
        {

            var account = _repo.GetUserByUsername(userToLogin.Username);

            if (account == null || (!_accountService.VerifyPassword(userToLogin.Password, account.HashedPassword)))
            {
                throw new AppException("Incorrect Username and/or Password.");
            }

            var user = _mapper.Map<User>(account);

            var signedAndEncodedToken = _accountService.CreateJsonToken(user);

            _logger.LogInfo($"Token successfully created. Encoded as {signedAndEncodedToken}");
            return Ok(signedAndEncodedToken);
        }
    }
    
}