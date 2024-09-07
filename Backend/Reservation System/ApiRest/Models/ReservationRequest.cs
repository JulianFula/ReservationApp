namespace ApiRest.Models;

public class ReservationRequest
{
    public int ReservationId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int CustomerId { get; set; }
    public int ServiceId { get; set; }
}
