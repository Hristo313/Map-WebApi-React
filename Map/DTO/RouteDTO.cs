using Map.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Models
{
    public class RouteDTO
    {
        public int Id { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public int Length { get; set; }
    }
}
