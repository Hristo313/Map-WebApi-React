using Map.Data;
using Map.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Map.ValidationAttributes
{
    public class AddTownAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));           

            if (context.Towns.Any(t => t.Name == (string)value))
            {
                return new ValidationResult("You already have " + value);
            }

            return ValidationResult.Success;
        }
    }
}
