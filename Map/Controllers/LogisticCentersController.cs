﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Map.Data;
using Map.Models;
using Map.DTO;

namespace Map.Controllers
{
    [Route("api/LogisticCenters")]
    [ApiController]
    public class LogisticCentersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogisticCentersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LogisticCenters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogisticCenter>>> GetLogisticCenters()
        {
            return await _context.LogisticCenters.ToListAsync();
        }

        // GET: api/LogisticCenters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogisticCenter>> GetLogisticCenter(int id)
        {
            var logisticCenter = await _context.LogisticCenters.FindAsync(id);

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

            _context.Entry(logisticCenter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<LogisticCenter>> PostLogisticCenter([FromForm]GetTownsDTO[] towns)
        {


            foreach (var t in towns)
            {
                Console.WriteLine(t.Name);
            }

            return null;
            //_context.LogisticCenters.Add(logisticCenter);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetLogisticCenter), new { id = logisticCenter.Id }, logisticCenter);
        }

        // DELETE: api/LogisticCenters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogisticCenter(int id)
        {
            var logisticCenter = await _context.LogisticCenters.FindAsync(id);
            if (logisticCenter == null)
            {
                return NotFound();
            }

            _context.LogisticCenters.Remove(logisticCenter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogisticCenterExists(int id)
        {
            return _context.LogisticCenters.Any(e => e.Id == id);
        }
    }
}
