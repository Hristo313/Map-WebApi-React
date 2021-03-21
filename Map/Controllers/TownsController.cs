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
        private readonly ApplicationDbContext context;

        public TownsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Towns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Town>>> GetTowns()
        {
            return await this.context.Towns.ToListAsync();
        }

        // GET: api/Towns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Town>> GetTown(int id)
        {
            var town = await this.context.Towns.FindAsync(id);

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

            this.context.Entry(town).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
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
            this.context.Towns.Add(town);
            await this.context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTown), new { id = town.Id }, town);
        }

        // DELETE: api/Towns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTown(int id)
        {
            var town = await this.context.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }

            this.context.Towns.Remove(town);
            await this.context.SaveChangesAsync();

            return NoContent();
        }

        private bool TownExists(int id)
        {
            return this.context.Towns.Any(e => e.Id == id);
        }
    }
}
