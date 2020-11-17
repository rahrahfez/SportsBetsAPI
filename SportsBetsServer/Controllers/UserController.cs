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
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repo;    
        private readonly ILoggerManager _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UserController(
            IRepositoryWrapper repo, 
            ILoggerManager logger, 
            IUserService userService,
            IAuthService authService)
        {
            _logger = logger;
            _repo = repo;
            _authService = authService;
            _userService = userService;
        }
        [HttpGet]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            try
            {
                //var users = await _repo.User.FindAllAsync();
                var users = new[] { "user1", "user2" };
                _logger.LogInfo($"Returned all users from database.");

                return Ok(users);
            }
            catch(Exception ex)
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
                var user = await _repo.User.FindByGuidAsync(id);
                
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
        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterUser([FromBody]UserCredentials user)
        {
            try
            {
                if (_userService.UserExists(user.Username))
                {
                    _logger.LogError("Username already exists.");
                    return BadRequest("Username already exists.");
                }             
                if (user.Username == string.Empty || user.Password == string.Empty)
                {
                    _logger.LogError("User is null.");
                    return BadRequest("User object is null");
                }

                User createdUser = await _userService.CreateUserAsync(user);
                await _authService.CreateCredentialsAsync(createdUser, user.Password); 
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

                _repo.User.Delete(userToBeDeleted);

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