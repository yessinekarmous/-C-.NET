using System.ComponentModel.DataAnnotations;
namespace DateValidator.Models;
#pragma warning disable CS8618


public class User {
    [FutureDate]
    public DateTime myDate{get;set;}
}