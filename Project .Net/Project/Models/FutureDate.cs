#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Project.Models;
public class FutureDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (DateTime.Now <((DateTime)value) )
            return new ValidationResult("Birthdate be in the future");
        return ValidationResult.Success;
    }
}

