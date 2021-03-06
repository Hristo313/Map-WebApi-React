using System.ComponentModel.DataAnnotations;

namespace Map.Models
{
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Town Start { get; set; }

        [Required]
        public virtual Town End { get; set; }

        [Required]
        public int Length { get; set; }
    }
}
