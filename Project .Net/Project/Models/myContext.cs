#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
using Project.Models;
namespace Project.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; } 
    
    public DbSet<Appointment> Appointments { get; set; } 
    
    public DbSet<Evaluation> Evaluations { get; set; } 
}
