using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Map.Data;
using Map.Models;

namespace Map.Controllers
{
    [Route("api/Towns")]
    [ApiController]
    public class TownsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TownsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Towns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Town>>> GetTowns()
        {
            return await _context.Towns.ToListAsync();
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Town>> GetTown(int id)
        {
            var town = await _context.Towns.FindAsync(id);

            if (town == null)
            {
                return NotFound();
            }

            return town;
        }

       

        // PUT: api/Towns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(int id, Town town)
        {
            if (id != town.Id)
            {
                return BadRequest();
            }

            _context.Entry(town).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Towns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Town>> PostTown(Town town)
        {
            _context.Towns.Add(town);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTown), new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTown(int id)
        {
            var town = await _context.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }

            _context.Towns.Remove(town);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TownExists(int id)
        {
            return _context.Towns.Any(e => e.Id == id);
        }
    }
}
