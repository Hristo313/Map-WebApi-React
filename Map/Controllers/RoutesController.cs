using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Map.Data;
using Map.Models;
using Map.Services;

namespace Map.Controllers
{
    [Route("api/Routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IRouteService routeService;

        public RoutesController(ApplicationDbContext context, IRouteService routeService)
        {
            this.context = context;
            this.routeService = routeService;
        }

        // GET: api/Routes
        [HttpGet]
        public ActionResult<IEnumerable<RouteDTO>> GetRoutes()
        {          
            return this.routeService.GetRoutes();
        }

        // GET: api/Routes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Route>> GetRoute(int id)
        {
            var route = await this.context.Routes.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        // PUT: api/Routes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(int id, Route route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }

            this.context.Entry(route).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
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

        // POST: api/Routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{start}/{end}/{length}")]
        public async Task<ActionResult<Route>> PostRoute(string start, string end, int length)
        {
            var startTown = this.routeService.GetStartTown(start);
            var endTown = this.routeService.GetEndTown(end);

            if (startTown == null || endTown == null)
            {
                return NotFound();
            }

            var route = this.routeService.GetRoute(startTown, endTown, length);
            await this.context.Routes.AddAsync(route);
            await this.context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoute), new { id = route.Id }, route);
        }

        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await this.context.Routes.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            this.context.Routes.Remove(route);
            await this.context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteExists(int id)
        {
            return this.context.Routes.Any(e => e.Id == id);
        }
    }
}
