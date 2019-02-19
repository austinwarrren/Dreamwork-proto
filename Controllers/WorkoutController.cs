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
    public class WorkoutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/workout
        [HttpGet]
        public async Task<ActionResult<Workout>> GetToday() //Will need to convert DateTime.Now from UTC to whatever time zone the user is in, or require date input
        {
            return await _context.WorkoutData.Where(w => w.startDate <= DateTime.Now || w.endDate >= DateTime.Now).FirstAsync();
        }

        // GET api/workout/{startDate}
        [HttpGet("{startDate}")]
        public async Task<ActionResult<Workout>> Get(DateTime StartDate)
        {
            return await _context.WorkoutData.Where(w => w.startDate <= StartDate || w.endDate >= StartDate).FirstAsync(); 
        }

        // POST api/workout
        [HttpPost]
        public async Task<ActionResult<Workout>> Post(Workout workout)
        {
            var existingWorkout = await _context.WorkoutData.Where(w => w.startDate <= workout.startDate || w.endDate >= workout.startDate).FirstOrDefaultAsync();
            if(existingWorkout == null)
            {
                _context.WorkoutData.Add(workout); //Check workout JSON for endDate. Fill w/ null if necessary
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { StartDate = workout.startDate }, workout);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/workout/{startDate}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Workout>> Put(int id, Workout workout)
        {
            var workoutCheck = await _context.WorkoutData.FindAsync(id);
            if(workoutCheck == null || id != workout.Id)
            {
                return BadRequest();
            }

            // Remove tracking of entity state:
            _context.Entry(workoutCheck).State = EntityState.Detached;

            _context.Entry(workout).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/workout/{startDate}
        [HttpDelete("{startDate}")]
        public async Task<ActionResult<Workout>> Delete(DateTime StartDate)
        {
            var workout = await _context.WorkoutData.Where(w => w.startDate == StartDate).FirstOrDefaultAsync();

            if(workout == null)
            {
                return NotFound();
            }

            _context.WorkoutData.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}