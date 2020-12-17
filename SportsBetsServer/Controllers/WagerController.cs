using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Models.Account;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SportsBetsServer.Controllers
{
    [Route("api/wagers")]
    [Authorize]
    [ApiController]
    public class WagerController : BaseController
    {
        private readonly IRepositoryBase<Wager> _repo;
        private readonly ILoggerManager _logger;
        private readonly IWagerService _wagerService;
        
        public WagerController(
            IRepositoryBase<Wager> repo, 
            ILoggerManager logger,
            IWagerService wagerService)
        {
            _repo = repo;
            _logger = logger;
            _wagerService = wagerService;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllWagers()
        {   
            try
            {   
                var wagers = _repo.GetAll();
                
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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWagerByIdAsync(Guid id) 
        {
            try
            {
                var wager = await _repo.GetAsync(id);
             
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
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateWager([FromBody] WagerToCreate _wager)
        {
            try
            {
                Wager wager = new Wager();

                var user = new User
                {

                };


                if (user.AvailableBalance >= _wager.Amount)
                {
                    wager = _wagerService.CreateWager(_wager);
                }

                await _repo.AddAsync(wager);

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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteWager([FromBody]Wager wager)
        {
            if (wager.Id == Guid.Empty)
            {
                return NotFound();
            }
            try
            {
                var wagerToBeDeleted = await _repo.GetAsync(wager.Id);

                _repo.Remove(wagerToBeDeleted);
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