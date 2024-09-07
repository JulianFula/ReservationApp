using ApiRest.Models;
using ApiRest.Services.Login;
using ApiRest.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controllers;

/// <summary>
/// Controlador para gestionar los usuarios.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Inicializa una nueva instancia del controlador <see cref="UserController"/>.
    /// </summary>
    /// <param name="userService">Servicio de usuarios.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una lista de usuarios.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Obtiene una usuario específico por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <returns>El usuario correspondiente.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Crea una nuevo usuario.
    /// </summary>
    /// <param name="User">Detalles del usuario a crear.</param>
    /// <returns>El usuario creado.</returns>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">ID del usuario a actualizar.</param>
    /// <param name="reservation">Detalles actualizados del usuario.</param>
    /// <returns>Respuesta HTTP.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        try
        {
            await _userService.UpdateUserAsync(id, user);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Elimina un usuario específica por su ID.
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>Respuesta HTTP.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
