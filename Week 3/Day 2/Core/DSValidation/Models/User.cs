using System.ComponentModel.DataAnnotations;
namespace DSValidation.Models;
#pragma warning disable CS8618

public class User {
    [Display(Name ="Your Name :")]
    [MinLength(2,ErrorMessage ="Name must be at least 2 characters")]
    [Required(ErrorMessage ="The Name is required ,please")]
    public string Name {get ; set ;}

    [Required(ErrorMessage ="The location is required ,please")]
    [Display(Name ="Dojo Location :")]
    public string location {get ; set ;}

    [Required(ErrorMessage ="The Favourite language is required ,please")]
    [Display(Name ="Favourite language :")]
    public string language {get;set;}

    [Display(Name ="Comment :")]
    [MinLength(20,ErrorMessage ="Comment must be at least 20 characters")]
    public string comment  {get;set;}

}