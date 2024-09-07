namespace ApiRest.Services.Reservation;

public interface IReservationService
{
    Task<IEnumerable<Models.Reservation>> GetReservationsAsync();
    Task<Models.Reservation> GetReservationByIdAsync(int id);
    Task<Models.Reservation> CreateReservationAsync(Models.Reservation reservation);
    Task UpdateReservationAsync(int id, Models.Reservation reservation);
    Task DeleteReservationAsync(int id);
    Task<IEnumerable<Models.Reservation>> FilterReservationsAsync(DateTime? date, int? serviceId, int? customerId);
}
