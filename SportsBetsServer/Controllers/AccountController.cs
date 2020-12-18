using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using LoggerService;
using AutoMapper;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using SportsBetsServer.Helpers;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Policy = Policy.User)]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AccountController(
            IAccountRepository repo,
            IMapper mapper,
            ILoggerManager logger)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Role.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            var users = _repo.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}", Name = "UserById")]
        [Consumes("text/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<User>> GetUserById(Guid id) 
        {
            var account = await _repo.GetAsync(id);
            var user = _mapper.Map<User>(account);
                
            if (user.Id.Equals(Guid.Empty) || user == null) 
            {
                throw new NotFoundException("User not found.");
            }
            else
            {
                _logger.LogInfo($"Returned user with id: { id }");
                return user;
            }
        }
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAccount([FromBody]UserCredentials userCredentials)
        {
            try
            {
                var user = _repo.GetUserByUsername(userCredentials.Username);

                if (user != null)
                {
                    _logger.LogError("Username already exists.");
                    return BadRequest("Username already exists.");
                }             
                if (userCredentials.Username == string.Empty || userCredentials.Password == string.Empty)
                {
                    _logger.LogError("User is null.");
                    return BadRequest("User object is null");
                }

                Account account = new Account();

                await _repo.AddAsync(account);

                _logger.LogInfo($"Successfully registered { user.Username }.");

                return CreatedAtRoute(routeName: "UserById", routeValues: new { id = account.Id }, value: account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: { ex.Message }");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/balance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserAvailableBalanceById(Guid id)
        {
            try
            {
                var user = await _repo.GetAsync(id);
                await _repo.Complete();
                return Ok(user.AvailableBalance);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve available balance from GetUserAvailableBalance() { ex.Message }");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var userToBeDeleted = await _repo.GetAsync(id);

                if (userToBeDeleted == null)
                {
                    _logger.LogError($"User with id: { id } was not found");
                    return NotFound();
                }

                _repo.Remove(userToBeDeleted);
                await _repo.Complete();

                _logger.LogInfo($"User with id: { id } successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser(): { ex.Message }");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}