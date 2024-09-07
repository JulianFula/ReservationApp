namespace ApiRest.Models;

public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    // Navigation property
    public ICollection<Reservation> Reservations { get; set; }
}
