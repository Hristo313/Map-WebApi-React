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
            if (validationContext.MemberName.Equals(value))
            {
                return new ValidationResult("Invalid" + validationContext.DisplayName);
            }

            return ValidationResult.Success;
        }
    }
}
