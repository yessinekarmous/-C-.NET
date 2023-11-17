using System.ComponentModel.DataAnnotations;
namespace DateValidator.Models;
#pragma warning disable CS8618
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime? dateValue = value as DateTime?;
        
                DateTime currentDate = DateTime.Now;

                if (dateValue > currentDate)
                {
                    return ValidationResult.Success!;
                }
                else
                {
                    
                    return new ValidationResult("La date doit être ultérieure à la date actuelle.");
                }
            
        }
    
}

