public class Booking
{
    public int Id { get; set; }
    public string? Name { get; set; }       // Nullable Name
    public string? Phone { get; set; }      // Nullable Phone
    public DateOnly? Dob { get; set; }      // Nullable Date of Birth
    public string? Address { get; set; }    // Nullable Address
    public string? Department { get; set; } // Nullable Department
    public DateOnly? BookingDate { get; set; } // Nullable BookingDate
}