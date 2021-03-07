using Map.Data;
using Map.DTO;
using Map.Models;
using System.Collections.Generic;
using System.Linq;

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
            return _context.Routes.Where(r => towns.Towns.Any(t => t.Name == r.Start.Name || t.Name == r.End.Name)).ToList();
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
            _context.Regions.Add(region);
            _context.SaveChanges();

            return region;
        }
    }
}
