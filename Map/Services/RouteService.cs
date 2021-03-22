using Map.Data;
using Map.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Services
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext context;

        public RouteService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Town GetStartTown(string start)
        {
            return this.context.Towns.Where(t => start == t.Name).FirstOrDefault();
        }

        public Town GetEndTown(string end)
        {
           return this.context.Towns.Where(t => end == t.Name).FirstOrDefault();
        }

        public Route GetRoute(Town startTown, Town endTown, int length)
        {
            Route route = new Route();
            route.Start = startTown;
            route.End = endTown;
            route.Length = length;

            return route;
        }

        public ActionResult<IEnumerable<RouteDTO>> GetRoutes()
        {
            return this.context.Routes.Select(x => new RouteDTO
            {
                Id = x.Id,
                Start = x.Start.Name,
                End = x.End.Name,
                Length = x.Length
            })
            .ToList();
        }
    }
}
