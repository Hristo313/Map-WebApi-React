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

        public void DFS(int node, int length, ref int maxLength, ref string maxLengthTownName, List<Route> allRoutes, Dictionary<int, string> longestRoutes, Boolean[] visited)
        {
            visited[node] = true;

            for (int j = 0; j < allRoutes.Count(); j++)
            {
                if (!visited[allRoutes[node].End.Id])
                {
                    length += allRoutes[node].Length;
                    DFS(allRoutes[node].End.Id, length, ref maxLength, ref maxLengthTownName, allRoutes, longestRoutes, visited);
                }
            }

            if (length > maxLength)
            {
                maxLength = length;
                maxLengthTownName = allRoutes[node].End.Name;
            }
        }

        public LogisticCenter FindLogisticCenter(Region region)
        {
            var longestRoutes = new Dictionary<int, string>();

            var allTowns = region.Towns.Count();
            var allRoutes = region.Routes.ToList();

            Boolean[] visited = null;
            int length = 0;
            int maxLength = int.MinValue;
            string maxLengthTownName = string.Empty;

            for (int i = 0; i < allTowns; i++)
            {
                length = 0;
                maxLength = int.MinValue;
                visited = new Boolean[allTowns + 1];

                if (!visited[i])
                {
                    DFS(i, length, ref maxLength, ref maxLengthTownName, allRoutes, longestRoutes, visited);
                    longestRoutes.Add(maxLength, maxLengthTownName);
                }
            }

            var logisticCenter = new LogisticCenter();
            var shortestRoute = longestRoutes.OrderByDescending(x => x.Key).First().Value;
            logisticCenter.Name = shortestRoute;

            return logisticCenter;
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

            var isValidStartRoute = false;
            var isValidEndRoute = false;

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
                townsCollection
                    .Add(_context.Towns
                    .Where(t => t.Name == town.Name)
                    .FirstOrDefault());
            }

            region.Towns = townsCollection;

            foreach (var route in routes)
            {
                routesCollection
                    .Add(_context.Routes
                    .Where(r => r.Start.Name == route.Start.Name && r.End.Name == route.End.Name)
                    .FirstOrDefault());
            }

            region.Routes = routesCollection;

            var passTowns = true;

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
