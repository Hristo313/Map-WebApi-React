using Map.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Services
{
    public interface IRouteService
    {
        Town GetStartTown(string start);

        Town GetEndTown(string end);

        Route GetRoute(Town startTown, Town endTown, int length);

        ActionResult<IEnumerable<RouteDTO>> GetRoutes();
    }
}
