#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNdishes.Models;
public class MinimumAge : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (DateTime.Now.Year-((DateTime)value).Year <18)
            return new ValidationResult("Age must be over 18");
        return ValidationResult.Success;
    }
}

