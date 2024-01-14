#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models;


public class Appointment{
    
    [Key]
    public int AppointmentId {get;set;}

    [Required(ErrorMessage ="The appointment Time is required")]
    [FutureDate]
    public DateTime Time{get;set;}
    public string Attendance{get;set;}="absent";

    public int UserId{get;set;}

    public User? Client {get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;

}
// #pragma warning disable CS8618
// using System;
// using System.ComponentModel.DataAnnotations;

// namespace Project.Models
// {
//     public class Appointment
//     {
//         [Key]
//         public int AppointmentId { get; set; }

//         [Required(ErrorMessage = "The appointment Time is required")]
//         [FutureDate]
//         public DateTime Time { get; set; }

//         public string Attendance { get; set; } = "absent";
//         public int UserId { get; set; }
//         public User? Client { get; set; }
//         public DateTime CreatedAt { get; set; } = DateTime.Now;
//         public DateTime UpdatedAt { get; set; } = DateTime.Now;
//     }

//     public class DatePick
//     {
//         // [FutureDate]
//         public DateTime TheDate { get; set; } = new DateTime(2024, 1, 1, 8, 0, 0);
//     }

//     public class AppointmentDateViewModel
//     {
//         public Appointment Appointment { get; set; }
//         public Date Date { get; set; }
//     }
// }
