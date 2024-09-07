namespace ApiRest.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int CustomerId { get; set; }
    public int ServiceId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public Customer Customer { get; set; }
    public Service Service { get; set; }
}
