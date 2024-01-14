namespace Project.Models
{
    public class DatePick
    {
        [FutureDate]
        public DateTime TheDate { get; set; } 
    }
}