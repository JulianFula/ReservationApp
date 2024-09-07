using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Models;
using ApiRest.Data;

namespace ApiRest.Services.User;

public class UserService : IUserService
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    //Se crea un constructor para retornar la informacion en las variables
    public UserService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Models.User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Models.User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<Models.User> CreateUserAsync(Models.User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserAsync(int id, Models.User user)
    {
        if (id != user.UserId)
        {
            throw new ArgumentException("ID mismatch");
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
