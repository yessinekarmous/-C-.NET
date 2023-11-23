using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
namespace CRUDelicious.Models;

public class Dish{
    
    [Key]
    [Required]
    public int ItemId {get ; set ;}

    [Required(ErrorMessage ="The Chef's Name is required")]
    public string ChefName {get; set;}

    [Required(ErrorMessage ="The name of the Dish is required")]
    public string DishName {get;set;}
    
    [Required(ErrorMessage ="The number of calories is required")]
    public int Calories {get;set;}

    
    [Required(ErrorMessage ="The Tastiness is required")]
    [Range(1,5)]    
    public int Tastiness {get;set;}

    [Required(ErrorMessage ="The Description is required ")]
    public string Description {get;set;}

    public DateTime CreatedAt {get;set;}=DateTime.Now;
    public DateTime UpdatedAt {get;set;}=DateTime.Now;


}