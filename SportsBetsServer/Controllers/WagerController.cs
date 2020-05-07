using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using LoggerService;
using SportsBetsServer.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SportsBetsServer.Controllers
{
    [Route("api/wagers")]
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
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllWagers()
        {   
            try
            {   
                var wagers = await _repo.Wager.FindAllAsync();
                
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWagerByIdAsync(Guid id) 
        {
            try
            {
                var wager = _repo.Wager.FindByGuidAsync(id);
                
                await _repo.Complete();

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWager(Wager wager)
        {
            try
            {
                wager = new Wager
                {
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Status = Status.open,
                    WinCondition = wager.WinCondition,
                    Amount = wager.Amount,
                };

                await _repo.Wager.CreateAsync(wager);
                await _repo.Complete();

                _logger.LogInfo($"Successfully created wager with id {wager.Id}");
                return CreatedAtRoute("WagerById", wager);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in CreateWager(): {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWager([FromBody]Wager wager)
        {
            if (wager.Id == Guid.Empty)
            {
                return NotFound();
            }
            try
            {
                var wagerToBeDeleted = await _repo.Wager.FindByGuidAsync(wager.Id);

                _repo.Wager.Delete(wagerToBeDeleted);
                await _repo.Complete();

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