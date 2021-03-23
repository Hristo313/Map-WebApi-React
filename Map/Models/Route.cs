using Map.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Map.Models
{
    public class Route : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Town Start { get; set; }

        [Required]
        public virtual Town End { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public int Length { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            if (context.Routes.Any(r => r.Start == this.Start && r.End == this.End || r.Start == this.End && r.End == this.Start))
            {
                yield return new ValidationResult("You already have this route!");
            }
        }
    }
}
