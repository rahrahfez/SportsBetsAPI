using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsBetsServer.Repository;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    public class CounterController : BaseController
    {    
        private readonly RepositoryContext _context;
        public CounterController(
            RepositoryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCount()
        {
            var count = _context.Counter.Find(1);
            return Ok(count);
        }
        [HttpPost("increment")]
        public IActionResult Increment()
        {
            var count = _context.Counter.Find(1);
            count.Count = count.Count + 1;
            _context.Counter.Update(count);
            _context.SaveChanges();
            return Ok(count);

        }
        [HttpPost("decrement")]
        public IActionResult Decrement()
        {
            var count = _context.Counter.Find(1);
            count.Count = count.Count - 1;
            _context.Counter.Update(count);
            _context.SaveChanges();
            return Ok(count);
        }
    }
}
