using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dreamwork_proto.Models;

namespace dreamwork_proto.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RunController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/run/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Run>> Get(int id)
        {
            return await _context.RunData.FindAsync(id);
        }

        // GET api/run
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Run>>> GetCurrentUserRunData()
        {
            var userId = 3;  // Change to be loaded by Okta API
            return await _context.RunData.Where(r => r.UserId == userId).ToListAsync();
        }
       
        // GET api/run/user/{UserId}
        [HttpGet("user/{UserId}")]
        public async Task<ActionResult<IEnumerable<Run>>> GetUserRunData(int userId)
        {
            return await _context.RunData.Where(r => r.UserId == userId).ToListAsync();
        }

        // POST api/run
        [HttpPost]
        public async Task<ActionResult<Run>> Post(Run run)
        {
            // Add UserId to the object before adding to the DB:
            _context.RunData.Add(run);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = run.Id }, run);
        }

        // PUT api/run/{Id}
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int id, Run run)
        {
            var runCheck = await _context.RunData.FindAsync(id);
            if(id != run.Id || runCheck == null)
            {
                return BadRequest();
            }

            // Remove tracking of entity state:
            _context.Entry(runCheck).State = EntityState.Detached;

            _context.Entry(run).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/run/{Id}
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var run = await _context.RunData.FindAsync(id);

            if(run == null)
            {
                return NotFound();
            }

            _context.RunData.Remove(run);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}