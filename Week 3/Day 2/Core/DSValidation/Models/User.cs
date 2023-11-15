using System.ComponentModel.DataAnnotations;
namespace FirstMVC.Models;
#pragma warning disable CS8618

public class User{
    [Required(ErrorMessage ="The Name is required, please")]
public string Name;

    [Required(ErrorMessage ="Pick the location, please")]
    public string location;
    [Required(ErrorMessage ="Pick the language, please")]
    public string language;
    public string comment ;


}