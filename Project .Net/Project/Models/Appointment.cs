#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Models;

public enum AttendanceStatus
{
    Absent,
    Present
}
public class Appointment{
    
    [Key]
    public int AppointmentId {get;set;}

    [Required(ErrorMessage ="The appointment Time is required")]
    [FutureDate]
    public DateTime Time{get;set;}

    public AttendanceStatus Attendance{get;set;}=AttendanceStatus.Absent;

    public int UserId{get;set;}

    public User? Client {get;set;}

    public DateTime CreatedAt{get;set;}=DateTime.Now;
    public DateTime UpdatedAt{get;set;}=DateTime.Now;



    






}