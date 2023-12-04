#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ManyToMany.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public int Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association> myCategories{ get; set; }= new List<Association>();
}
