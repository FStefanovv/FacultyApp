using System.ComponentModel.DataAnnotations;

namespace FacultyApp.Attributes;

public class FutureDateAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
        if (value != null)
        {
            DateTime dateValue = (DateTime)value;

            if (dateValue <= DateTime.UtcNow)
            {
                return new ValidationResult("Entered date must be in the future.");
            }
        }

        return ValidationResult.Success;
    }
}