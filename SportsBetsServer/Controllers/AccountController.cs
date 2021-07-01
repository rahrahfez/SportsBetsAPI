using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using SportsBetsServer.Contracts.Services;
using System.Threading.Tasks;
using AutoMapper;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        [HttpGet, Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            var users = _accountService.GetAllUsers();
            return Ok(users);
        }
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAccount([FromBody]UserCredentials userCredentials)
        {
            var account = await _accountService.RegisterNewAccount(userCredentials);
            if (account == null)
            {
                return BadRequest("Username already exists.");
            }    

            var user = _mapper.Map<User>(account);
            var token = _accountService.CreateJsonToken(user);

            return CreatedAtRoute(routeName: "UserById", routeValues: new { id = user.Id }, value: token);               

        }
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var account = _accountService.Authenticate(userCredentials);
            if (account == null)
            {
                return Unauthorized();
            }
            var authenticatedUser = _mapper.Map<User>(account);
            var token = _accountService.CreateJsonToken(authenticatedUser);
            return Ok(token);
        }
        [HttpGet("{id}/balance"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserAvailableBalanceById(Guid id)
        {
            var account = await _accountService.GetAccountById(id);
            var balance = account.AvailableBalance;
            return Ok(balance);
        }
        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult DeleteUser(Guid id)
        {
            _accountService.DeleteUserById(id);
            return NoContent();
        }
    }
}