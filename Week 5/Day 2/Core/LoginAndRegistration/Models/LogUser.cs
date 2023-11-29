#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace LoginAndRegistration.Models;

public class LogUser{
    [Required]
    [EmailAddress]
    public string LogEmail {get;set;}

    [Required]
    [DataType(DataType.Password)]
    public string LogPassword {get;set;}

}