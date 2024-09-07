using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ApiRest.Services.Login;

namespace ApiRest.Controllers;

/// <summary>
/// Controlador para gestionar la autentificacion y el registro.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Inicializa una nueva instancia del controlador <see cref="AuthController"/>.
    /// </summary>
    /// <param name="authService">Servicio de autentificacion.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Permite logearse y obtener un token de autorizacion.
    /// </summary>
    /// <returns>Token de autorizacion.</returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> login([FromBody] Models.Login.AuthRequest autorization)
    {

        var result = await _authService.ReturnToken(autorization);
        if (result == null)
        {
            return Unauthorized();
        }

        return Ok(result);

    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="AuthRequest">Detalles del Usuario a crear.</param>
    /// <returns>El usuario creado.</returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Models.Login.AuthRequest autorization)
    {

        var register = await _authService.Register(autorization);

        return Ok(register);
    }

}
