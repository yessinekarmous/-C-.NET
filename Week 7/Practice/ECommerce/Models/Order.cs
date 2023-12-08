#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
namespace ECommerce.Models;
public class Order{
    [Key]
    public int OrderId{get;set;}
    [Range(1,int.MaxValue,ErrorMessage ="Invalid Quantity")]
    public int Quantity{get;set;}=1;

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;
    [Required]
    public int CustomerId{get;set;}
    public Customer? customer{get;set;}
    [Required]
    public int ProductId{get;set;}
    public Product? product{get;set;}

}