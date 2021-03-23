using Map.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Map.ValidationAttributes
{
    public class CapitalLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var town = (Town)validationContext.ObjectInstance;

            if (!char.IsUpper(town.Name.First()))
            {
                return new ValidationResult("Invalid name");
            }

            return ValidationResult.Success;
        }
    }
}
