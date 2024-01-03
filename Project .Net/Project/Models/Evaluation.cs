#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models;
public class Evaluation{
    
    [Key]
    public int EvaluationId {get;set;}

    [Required(ErrorMessage ="The Evaluation is required")]
    public string EvaluationText{get;set;}

    public int UserId{get;set;}

    public User? Creator {get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;











}