using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Map.Data;
using Map.Models;
using Map.DTO;
using Map.Services;

namespace Map.Controllers
{
    [Route("api/LogisticCenters")]
    [ApiController]
    public class LogisticCentersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogisticCenterService logisticCenterService;

        public LogisticCentersController(ApplicationDbContext context, ILogisticCenterService logisticCenterService)
        {
            this.context = context;
            this.logisticCenterService = logisticCenterService;
        }

        // GET: api/LogisticCenters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogisticCenter>>> GetLogisticCenters()
        {
            return await this.context.LogisticCenters.ToListAsync();
        }

        // GET: api/LogisticCenters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogisticCenter>> GetLogisticCenter(int id)
        {
            var logisticCenter = await this.context.LogisticCenters.FindAsync(id);

            if (logisticCenter == null)
            {
                return NotFound();
            }

            return logisticCenter;
        }

        // PUT: api/LogisticCenters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogisticCenter(int id, LogisticCenter logisticCenter)
        {
            if (id != logisticCenter.Id)
            {
                return BadRequest();
            }

            this.context.Entry(logisticCenter).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogisticCenterExists(id))
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

        // POST: api/LogisticCenters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetTownsDTO>> PostLogisticCenter(GetTownsDTO towns)
        {
            var routes = this.logisticCenterService.FindRoutes(towns);
            var region = this.logisticCenterService.MakeRegion(towns, routes);

            if (region == null)
            {
                throw new InvalidOperationException("You already have this region!");
            }

            var logisticCenter = this.logisticCenterService.FindLogisticCenter(region);

            this.context.LogisticCenters.Add(logisticCenter);
            await this.context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogisticCenter), new { id = logisticCenter.Id }, logisticCenter);
        }

        // DELETE: api/LogisticCenters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogisticCenter(int id)
        {
            var logisticCenter = await this.context.LogisticCenters.FindAsync(id);
            if (logisticCenter == null)
            {
                return NotFound();
            }

            this.context.LogisticCenters.Remove(logisticCenter);
            await this.context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogisticCenterExists(int id)
        {
            return this.context.LogisticCenters.Any(e => e.Id == id);
        }
    }
}
