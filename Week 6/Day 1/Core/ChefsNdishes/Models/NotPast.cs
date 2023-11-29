#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNdishes.Models;
public class NotPast : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (DateTime.Now <((DateTime)value) )
            return new ValidationResult("Birthdate must not be in the past");
        return ValidationResult.Success;
    }
}

