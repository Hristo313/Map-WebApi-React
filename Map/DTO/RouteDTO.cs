using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Models
{
    public class RouteDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Start { get; set; }

        [Required]
        [MinLength(3)]
        public string End { get; set; }

        [Required]
        [MinLength(1)]
        public int Length { get; set; }
    }
}
