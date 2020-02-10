using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using LoggerService;
using SportsBetsServer.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportsBetsServer.Controllers
{
    [Route("api/wager")]
    [Authorize]
    [ApiController]
    public class WagerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repo;
        private readonly ILoggerManager _logger;
        
        public WagerController(IRepositoryWrapper repo, ILoggerManager logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet("wagers")]
        public async Task<IActionResult> GetAllWagers()
        {   
            try
            {   
                var wagers = await _repo.Wager.GetAllWagersAsync();

                _logger.LogInfo("Successfully retrieved all wagers");

                return Ok(wagers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllWagers(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}", Name = "WagerById")]
        public async Task<IActionResult> GetWagerByIdAsync(Guid id) 
        {
            try
            {
                var wager = await _repo.Wager.GetWagerByIdAsync(id);
                _logger.LogInfo($"Successfully retrieved wager with id {id}");
                return Ok(wager);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetWagerAsync(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateWager(Guid userId)
        {
            try
            {
                var wager = new Wager
                {
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                };

                await _repo.Wager.CreateWagerAsync(wager);
                _logger.LogInfo($"Successfully created wager with id {wager.Id}");
                return CreatedAtRoute("WagerById", wager);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in CreateWager(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWager(Guid id, [FromBody]Wager wager)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return NotFound();
                }
                var wagerToBeUpdated = await _repo.Wager.GetWagerByIdAsync(id);

                await _repo.Wager.UpdateWagerAsync(wagerToBeUpdated, wager);

                return Ok(wager);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in UpdateWager(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWager([FromBody]Wager wager)
        {
            if (wager.Id == Guid.Empty)
            {
                return NotFound();
            }
            try
            {
                var wagerToBeDeleted = await _repo.Wager.GetWagerByIdAsync(wager.Id);
                await _repo.Wager.DeleteWagerAsync(wagerToBeDeleted);
                _logger.LogInfo($"Successfully deleted wager with id {wager.Id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in DeleteWager(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}