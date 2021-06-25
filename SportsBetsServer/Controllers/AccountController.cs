using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LoggerService;
using AutoMapper;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using SportsBetsServer.Helpers;
using SportsBetsServer.Repository;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly RepositoryContext _context;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AccountController(
            RepositoryContext context,
            IAccountService service,
            IMapper mapper,
            ILoggerManager logger)
        {
            _logger = logger;
            _context = context;
            _service = service;
            _mapper = mapper;
        }
        [HttpGet, Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            var accounts = _context.Account.ToList();
            var users = new List<User>();
            foreach (var account in accounts)
            {
                var user = _mapper.Map<User>(account);
                users.Add(user);
            }
            return Ok(users);
        }
        [HttpGet("{id}", Name = "AccountById"), Authorize, Consumes("text/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Account>> GetAccountById(Guid id) 
        {
            var account = await _context.Account.FindAsync(id);
                
            if (account.Id.Equals(Guid.Empty) || account == null) throw new NotFoundException("Account not found.");
            return Ok(account);
        }
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAccount([FromBody]UserCredentials userCredentials)
        {
            var account = _context.Account.Where(x => x.Username.Equals(userCredentials.Username)).SingleOrDefault();

            if (account != null)
            {
                _logger.LogError("Username already exists.");
                return BadRequest("Username already exists.");
            }             
            if (userCredentials.Username == string.Empty || userCredentials.Password == string.Empty)
            {
                _logger.LogError("User is null.");
                return BadRequest("User object is null");
            }

            Account newAccount = _service.CreateNewAccount(userCredentials);

            await _context.Account.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            var user = _mapper.Map<User>(newAccount);

            _logger.LogInfo($"Successfully registered { user.Username }.");
            return CreatedAtRoute(routeName: "UserById", routeValues: new { id = user.Id }, value: user);

        }
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var authenticatedUser = _service.Authenticate(userCredentials);               
            return Ok(authenticatedUser);
        }
        [HttpGet("{id}/balance"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserAvailableBalanceById(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                var user = _mapper.Map<User>(account);
                return Ok(user.AvailableBalance);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userToBeDeleted = await _context.Account.FindAsync(id);

            if (userToBeDeleted == null)
            {
                _logger.LogError($"User with id: { id } was not found");
                return NotFound();
            }

            _context.Account.Remove(userToBeDeleted);
            await _context.SaveChangesAsync();

            _logger.LogInfo($"User with id: { id } successfully deleted.");
            return NoContent();
        }
    }
}