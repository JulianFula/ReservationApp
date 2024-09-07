using ApiRest.Models.Login;
using Microsoft.AspNetCore.Identity.Data;

namespace ApiRest.Services.Login;

public interface IAuthService
{
    //Se crea el servicio para devolver el token de autorizacion
    Task<AuthResponse> ReturnToken(AuthRequest autorization);
    Task<AuthResponse> Register(AuthRequest autorization);
}
