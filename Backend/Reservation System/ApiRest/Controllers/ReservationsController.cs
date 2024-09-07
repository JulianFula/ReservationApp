using ApiRest.Models;
using ApiRest.Services.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRest.Controllers;

/// <summary>
/// Controlador para gestionar las reservas.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    /// <summary>
    /// Inicializa una nueva instancia del controlador <see cref="ReservationsController"/>.
    /// </summary>
    /// <param name="reservationService">Servicio de reserva.</param>
    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /// <summary>
    /// Obtiene todas las reservas.
    /// </summary>
    /// <returns>Una lista de reservas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
    {
        var reservations = await _reservationService.GetReservationsAsync();
        return Ok(reservations);
    }

    /// <summary>
    /// Obtiene una reserva específica por su ID.
    /// </summary>
    /// <param name="id">ID de la reserva.</param>
    /// <returns>La reserva correspondiente.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Reservation>> GetReservation(int id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }

    /// <summary>
    /// Crea una nueva reserva.
    /// </summary>
    /// <param name="reservationRequest">Detalles de la reserva a crear.</param>
    /// <returns>La reserva creada.</returns>
    [HttpPost]
    public async Task<ActionResult<Reservation>> CreateReservation(ReservationRequest reservationRequest)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        Reservation reservation = new Reservation()
        {
            CustomerId = reservationRequest.CustomerId,
            ReservationId = reservationRequest.ReservationId,
            ServiceId = reservationRequest.ServiceId,
            ReservationDate = reservationRequest.ReservationDate,
            UserId = userId
        };
        var createdReservation = await _reservationService.CreateReservationAsync(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.ReservationId }, createdReservation);
    }

    /// <summary>
    /// Actualiza una reserva existente.
    /// </summary>
    /// <param name="id">ID de la reserva a actualizar.</param>
    /// <param name="reservation">Detalles actualizados de la reserva.</param>
    /// <returns>Respuesta HTTP.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(int id, Reservation reservation)
    {
        try
        {
            await _reservationService.UpdateReservationAsync(id, reservation);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Elimina una reserva específica por su ID.
    /// </summary>
    /// <param name="id">ID de la reserva a eliminar.</param>
    /// <returns>Respuesta HTTP.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        try
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Filtra las reservas según los parámetros proporcionados.
    /// </summary>
    /// <param name="date">Fecha de la reserva.</param>
    /// <param name="serviceId">ID del servicio.</param>
    /// <param name="customerId">ID del cliente.</param>
    /// <returns>Una lista de reservas filtradas.</returns>
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Reservation>>> FilterReservations(DateTime? date, int? serviceId, int? customerId)
    {
        var filteredReservations = await _reservationService.FilterReservationsAsync(date, serviceId, customerId);
        return Ok(filteredReservations);
    }
}
