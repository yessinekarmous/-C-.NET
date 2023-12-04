#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ManyToMany.Models;
public class Category   
{
    [Key]
    public int CategoryId { get; set; }
    public string Name { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association> myProducts{ get; set; }= new List<Association>();
}
