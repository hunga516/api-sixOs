namespace api_sixOs.Models;

public class ChairBookings
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public int ChairNumber { get; set; }
    public int StartTime { get; set; } 
    public int EndTime { get; set; }   
    public DateOnly DateBooking { get; set; }
}