using Map.DTO;
using Map.Models;
using System.Collections.Generic;

namespace Map.Services
{
    public interface ILogisticCenterService 
    {
        LogisticCenter FindLogisticCenter(Region region);

        Region MakeRegion(GetTownsDTO towns, ICollection<Route> routes);

        ICollection<Route> FindRoutes(GetTownsDTO towns);
    }
}
