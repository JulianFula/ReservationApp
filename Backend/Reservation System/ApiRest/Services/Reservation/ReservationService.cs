using ApiRest.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Services.Reservation;

public class ReservationService : IReservationService
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    //Se crea un constructor para retornar la informacion en las variables
    public ReservationService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Models.Reservation>> GetReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Models.Reservation> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<Models.Reservation> CreateReservationAsync(Models.Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task UpdateReservationAsync(int id, Models.Reservation reservation)
    {
        if (id != reservation.ReservationId)
        {
            throw new ArgumentException("ID mismatch");
        }

        _context.Entry(reservation).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            throw new KeyNotFoundException("Reservation not found");
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Models.Reservation>> FilterReservationsAsync(DateTime? date, int? serviceId, int? customerId)
    {
        var query = _context.Reservations.AsQueryable();

        if (date.HasValue)
            query = query.Where(r => r.ReservationDate.Date == date.Value.Date);

        if (serviceId.HasValue)
            query = query.Where(r => r.ServiceId == serviceId);

        if (customerId.HasValue)
            query = query.Where(r => r.CustomerId == customerId);

        return await query.ToListAsync();
    }
}
