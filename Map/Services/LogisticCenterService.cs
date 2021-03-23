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
        private readonly ApplicationDbContext context;

        public LogisticCenterService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DFS(int node, int length, ref int maxLength, ref string maxLengthTownName, List<Route> allRoutes, Boolean[] visited)
        {
            visited[node] = true;

            for (int j = 0; j < allRoutes.Count(); j++)
            {
                if (!visited[allRoutes[j].End.Id])
                {
                    length += allRoutes[j].Length;

                    if (length > maxLength)
                    {
                        maxLength = length;
                        maxLengthTownName = allRoutes[j].End.Name;
                    }

                    DFS(allRoutes[j].End.Id, length, ref maxLength, ref maxLengthTownName, allRoutes, visited);
                }
            }
        }

        public LogisticCenter FindLogisticCenter(Region region)
        {
            var longestRoutes = new Dictionary<int, string>();

            int allRegionTowns = region.Towns.Count();
            var allRoutes = region.Routes.ToList();

            Boolean[] visited = null;
            int length = 0;
            int maxLength = int.MinValue;
            string maxLengthTownName = string.Empty;
            int allTowns = this.context.Towns.Count();

            for (int i = 1; i <= allRegionTowns; i++)
            {
                length = 0;
                maxLength = int.MinValue;
                visited = new Boolean[allTowns + 1];

                if (!visited[i])
                {
                    DFS(i, length, ref maxLength, ref maxLengthTownName, allRoutes, visited);
                    longestRoutes.Add(maxLength, maxLengthTownName);
                }
            }

            var logisticCenter = new LogisticCenter();
            string shortestRoute = longestRoutes.OrderByDescending(x => x.Key).First().Value;
            logisticCenter.Name = shortestRoute;

            return logisticCenter;
        }

        public ICollection<Route> FindRoutes(GetTownsDTO towns)
        {
            var allRoutes = this.context.Routes
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
                isValidStartRoute = false;
                isValidEndRoute = false;

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

                if (isValidStartRoute && isValidEndRoute)
                {
                    currentRoutes.Add(route);
                }                
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
                    .Add(this.context.Towns
                    .Where(t => t.Name == town.Name)
                    .FirstOrDefault());
            }

            region.Towns = townsCollection;

            foreach (var route in routes)
            {
                routesCollection
                    .Add(this.context.Routes
                    .Where(r => r.Start.Name == route.Start.Name && r.End.Name == route.End.Name)
                    .FirstOrDefault());
            }

            region.Routes = routesCollection;

            bool passTowns = true;

            foreach (var reg in this.context.Regions.ToList())
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

                if (!passTowns)
                {
                    return null;
                }
            }

            this.context.Regions.Add(region);
            this.context.SaveChanges();

            return region;
        }
    }
}
