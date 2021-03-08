using Map.Data;
using Map.DTO;
using Map.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Map.Services
{
    public class LogisticCenterService : ILogisticCenterService
    {
        private readonly ApplicationDbContext _context;

        public LogisticCenterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public LogisticCenter FindLogisticCenter(Region region)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Route> FindRoutes(GetTownsDTO towns)
        {
            var allRoutes = _context.Routes
                .Select(r => new Route
                { 
                    Start = r.Start,
                    End = r.End
                })
                .ToList();

            var currentRoutes = new List<Route>();

            bool isValidStartRoute = false;
            bool isValidEndRoute = false;

            foreach (var route in allRoutes)
            {
                foreach (var town in towns.Towns)
                {
                    if (town.Name == route.Start.Name)
                    {
                        isValidStartRoute = true;
                        break;
                    }
                }

                if (!isValidStartRoute)
                {
                    continue;
                }

                foreach (var town in towns.Towns)
                {
                    if (town.Name == route.End.Name)
                    {
                        isValidEndRoute = true;
                        break;
                    }
                }

                if (!isValidEndRoute)
                {
                    continue;
                }

                currentRoutes.Add(route);
            }

            return currentRoutes;
        }

        public Region MakeRegion(GetTownsDTO towns, ICollection<Route> routes)
        {
            var region = new Region();
            var townsCollection = new List<Town>();
            var routesCollection = new List<Route>();

            foreach (var town in towns.Towns)
            {
                townsCollection.Add(_context.Towns.Where(t => t.Name == town.Name).FirstOrDefault());
            }

            region.Towns = townsCollection;

            foreach (var route in routes)
            {
                routesCollection.Add(_context.Routes.Where(r => r.Start.Name == route.Start.Name && r.End.Name == route.End.Name).FirstOrDefault());
            }

            region.Routes = routesCollection;

            bool passTowns = true;

            foreach (var reg in _context.Regions.ToList())
            {
                passTowns = false;

                foreach (var town in townsCollection)
                {
                    if (reg.Towns.Any(t => t.Name == town.Name))
                    {
                        continue;
                    }
                    else
                    {
                        passTowns = true;
                        break;
                    }
                }             
            }

            if (!passTowns)
            {
                return null;
            }

            _context.Regions.Add(region);
            _context.SaveChanges();

            return region;
        }
    }
}
