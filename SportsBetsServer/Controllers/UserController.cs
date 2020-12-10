using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using LoggerService;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SportsBetsServer.Controllers
{
    [Route("api/users")]
    [Authorize(Policy = Policy.User)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repo;    
        private readonly ILoggerManager _logger;
        private readonly IUserService _userService;
        public UserController(
            IRepositoryWrapper repo, 
            ILoggerManager logger, 
            IUserService userService)
        {
            _logger = logger;
            _repo = repo;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repo.User.GetAllUsers();

                _logger.LogInfo($"Returned all users from database.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers() action: { ex.Message }");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}", Name = "UserById")]
        [Consumes("text/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<User>> GetUserById(Guid id) 
        {
            try
            {
                var user = await _repo.User.GetUserByGuidAsync(id);
                
                if (user.Id.Equals(Guid.Empty)) 
                {
                    _logger.LogError($"User with id: { id } was not found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: { id }");
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById() action { ex.Message }");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterUser([FromBody]UserCredentials user)
        {
            try
            {
                if (await _userService.UserExists(user.Username))
                {
                    _logger.LogError("Username already exists.");
                    return BadRequest("Username already exists.");
                }             
                if (user.Username == string.Empty || user.Password == string.Empty)
                {
                    _logger.LogError("User is null.");
                    return BadRequest("User object is null");
                }

                User createdUser = _userService.CreateUser(user);

                await _repo.User.CreateAsync(createdUser);
                await _repo.Complete();

                _logger.LogInfo($"Successfully registered { user.Username }.");

                return CreatedAtRoute(routeName: "UserById", routeValues: new { id = createdUser.Id }, value: createdUser);
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
                var availablebalance = await _repo.User.GetUserAvailableBalanceAsync(id);
                return Ok(availablebalance);
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
                var userToBeDeleted = await _repo.User.FindByGuidAsync(id);

                if (userToBeDeleted == null)
                {
                    _logger.LogError($"User with id: { id } was not found");
                    return NotFound();
                }

                _repo.User.Remove(userToBeDeleted);

                await _repo.User.Complete();
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