#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UserDashboard.Models;
public class User{

    [Key]
    public int UserId {get;set;}
    [Required(ErrorMessage ="First Name is required")]
    public string FName{get;set;}
    
    [Required(ErrorMessage ="Last Name is required")]
    public string LName{get;set;}

    [Required]
    [EmailAddress]
    public string Email{get;set;}

    [Required]
    [DataType(DataType.Password)]
    public string Password{get;set;}

    [Required(ErrorMessage ="Confirm password is required")]
    [NotMapped]
    [Compare("Password",ErrorMessage ="Password and confirm password must match !")]
    [DataType(DataType.Password)]
    public string ConfirmPassword{get;set;}

    public string? UserLevel{get;set;}

    public string? Description {get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;

    public List<Message> MsgPosted{get;set;}=new List<Message>();
    public List<Comment> CommentsPosted{get;set;}= new List<Comment>();


}