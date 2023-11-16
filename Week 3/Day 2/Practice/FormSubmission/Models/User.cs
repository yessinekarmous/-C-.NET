using System.ComponentModel.DataAnnotations;
namespace FormSubmission.Models;
#pragma warning disable CS8618

public class User {
    [Display(Name ="First Name")]
    [Required(ErrorMessage ="The First Name is required ,please")]
    public string FName {get ; set ;}

    [Required(ErrorMessage ="The Last Name is required ,please")]
    [Display(Name ="Last Name :")]
    public string LName {get ; set ;}

    [Required(ErrorMessage ="The Age is required ,please")]
    [Range(1,120)]
    [Display(Name ="Age :")]
    public int Age {get;set;}

    [Required(ErrorMessage ="The Email is required ,please")]
    [ DataType(DataType.EmailAddress, ErrorMessage="Invalid Email")]
    [Display(Name ="Email :")]
    public string Email  {get;set;}


    [Required(ErrorMessage ="The Password is required ,please")]
    [DataType(DataType.Password, ErrorMessage ="Invalid Password")]
    [Display(Name ="Password :")]
    public string Password {get;set;}
}