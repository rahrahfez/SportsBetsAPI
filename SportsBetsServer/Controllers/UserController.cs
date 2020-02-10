using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using LoggerService;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace SportsBetsServer.Controllers
{
    [Route("api/user")]
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
            _userService = userService;
            _logger = logger;
            _repo = repo;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repo.User.GetAllUsersAsync();

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
        public async Task<IActionResult> GetUserById(Guid id) 
        {
            try
            {
                var user = await _repo.User.GetUserByIdAsync(id);
                
                if (user.Id.Equals(Guid.Empty)) 
                {
                    _logger.LogError($"User with id: { id } was not found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: { id }");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById() action { ex.Message }");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]User user)
        {
            try
            {
                if (_userService.UserExists(user.Username))
                {
                    user = null;
                    _logger.LogError("Username already exists.");
                    return BadRequest("Username already exists.");
                }             
                if (user == null)
                {
                    _logger.LogError("User is null.");
                    return BadRequest("User object is null");
                }

                user.Id = Guid.NewGuid();
                user.AvailableBalance = 100;
                user.DateCreated = DateTime.Now;

                await _repo.User.CreateUserAsync(user);

                _logger.LogInfo($"Successfully registered { user.Username }.");

                return CreatedAtRoute(routeName: "UserById", routeValues: new { id = user.Id }, value: user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: { ex.Message }");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/balance")]
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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var userToBeDeleted = await _repo.User.GetUserByIdAsync(id);

                if (userToBeDeleted == null)
                {
                    _logger.LogError($"User with id: { id } was not found");
                    return NotFound();
                }

                await _repo.User.DeleteUserAsync(userToBeDeleted);
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