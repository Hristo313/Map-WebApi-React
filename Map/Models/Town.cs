using System.ComponentModel.DataAnnotations;

namespace Map.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
