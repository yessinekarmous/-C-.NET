using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models;
#pragma warning disable CS8618
public class NotPast : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if((DateTime)value<DateTime.Now){
            return new ValidationResult("Date must be in the future");
        }
        return ValidationResult.Success;
    }
}

