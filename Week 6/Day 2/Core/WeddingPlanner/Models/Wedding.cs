#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models;
public class Wedding{

    [Key]
    public int WeddingId{get;set;}
    [Required(ErrorMessage ="The Wedder One is required please !")]
    public string WedderOne{get;set;}
    [Required(ErrorMessage ="The Wedder Two is required please !")]
    public string WedderTwo{get;set;}
    [Required(ErrorMessage ="The Date is required please !")]
    [NotPast]
    public DateTime Date{get;set;}
    
    [Required(ErrorMessage ="The Address is required please !")]
    public string Address{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;

    [Required]
    public int UserId{get;set;}
    public User? Creator{get;set;}

    public List<Attendance> Guests{get;set;}= new List<Attendance>();
}