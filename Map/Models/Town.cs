using Map.Data;
using Map.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Map.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [CapitalLetterAttribute]
        [AddTownAttribute]
        public string Name { get; set; }
    }
}
