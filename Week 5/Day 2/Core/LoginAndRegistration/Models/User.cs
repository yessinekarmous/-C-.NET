#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginAndRegistration.Models;

public class User{

    [Key]
    public int UserId {get;set;}

    [Required]
    public string FName {get;set;}

    [Required]
    public string LName {get;set;}

    [Required]
    [EmailAddress]
    public string Email {get;set;}

    [Required]
    [DataType(DataType.Password)]
    public string Password {get;set;}

    [DataType(DataType.Password)]
    [NotMapped]
    [Compare("Password")]
    public string ConfirmPass {get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now ;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;
}