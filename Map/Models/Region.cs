using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Models
{
    public class Region
    {
        public Region()
        {
            this.Towns = new HashSet<Town>();
            this.Routes = new HashSet<Route>();
        }

        public int Id { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
