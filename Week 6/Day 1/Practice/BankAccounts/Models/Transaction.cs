#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BankAccounts.Models;
public class Transaction{

    [Key]
    public int TransactionId {get;set;}
    [Required]
    public double Amount{get;set;}
    
    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;

    public int UserId{get;set;}

    public User? holder{get;set;}
} 