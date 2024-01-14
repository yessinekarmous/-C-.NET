#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Project.Models;
public class PastDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (DateTime.Now <((DateTime)value) )
            return new ValidationResult("Birthdate must be in the past");
        return ValidationResult.Success;
    }
}

