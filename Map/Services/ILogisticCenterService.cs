using Map.DTO;
using Map.Models;
using System;
using System.Collections.Generic;

namespace Map.Services
{
    public interface ILogisticCenterService 
    {
        void DFS(int node, int length, ref int maxLength, ref string maxLengthTownName, List<Route> allRoutes, Boolean[] visited);

        LogisticCenter FindLogisticCenter(Region region);

        Region MakeRegion(GetTownsDTO towns, ICollection<Route> routes);

        ICollection<Route> FindRoutes(GetTownsDTO towns);
    }
}
