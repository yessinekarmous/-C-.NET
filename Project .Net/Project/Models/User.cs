#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models;

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

    [Required(ErrorMessage ="The Phone Number is required")]
    public int PhoneNumber{get;set;}
    public int? ExtraPhoneNumber{get;set;}

    [Required(ErrorMessage ="The Address is required")]
    public string Address{get;set;}

    [Required(ErrorMessage ="The Date of birth is required")]
    [PastDate]
    public DateTime Birthdate{get;set;}
    public string UserRole{get;set;}="user";

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;

    public List<Appointment> myAppointments{get;set;}=new List<Appointment>();
    public List<Evaluation> myEvaluations{get;set;}=new List<Evaluation>();



}