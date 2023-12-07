#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TheWall.Models;
public class Message{

    [Key]
    public int MessageId {get;set;}

    [Required]
    public string MessageText{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;
    [Required]
    public int UserId{get;set;}
    public User? Poster{get;set;}
    public List<Comment> MsgComments{get;set;}=new List<Comment>();
    


}