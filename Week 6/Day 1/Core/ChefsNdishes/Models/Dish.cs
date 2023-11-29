#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNdishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    [Range(0,int.MaxValue,ErrorMessage ="Calories must be positive")]
    public int Calories { get; set; }

    [Range(1,5)]
    public string Tastiness { get; set; }
    [Required]
    public int ChefId { get; set; } 
    public Chef? Chef { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}