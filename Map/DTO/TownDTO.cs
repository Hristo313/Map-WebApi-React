using Map.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Map.DTO
{
    public class TownDTO
    {
        [Required]
        [MinLength(3)]
        [AddTownAttribute]
        public string Name { get; set; }
    }
}
