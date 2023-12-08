#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ECommerce.Models;
public class Customer{
    [Key]
    public int CustomerId{get;set;}
    [Required]
    public string CustomerName{get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;
    
    public List<Order> OrdersMade{get;set;} = new List<Order>();
    
}