#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNdishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required]
    public string FName { get; set; } 
    [Required]
    public string LName { get; set; }
    [Required]
    
    [NotPast]
    [MinimumAge]
    public DateTime Birth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Dish> ListOfDishes{ get; set; }=new List<Dish>();
}