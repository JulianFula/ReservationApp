using ApiRest.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Services.Service;

public class ServiceService : IServiceService
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    //Se crea un constructor para retornar la informacion en las variables
    public ServiceService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Models.Service>> GetServicesAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Models.Service> GetServiceByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<Models.Service> CreateServiceAsync(Models.Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task UpdateServiceAsync(int id, Models.Service service)
    {
        if (id != service.ServiceId)
        {
            throw new ArgumentException("ID mismatch");
        }

        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteServiceAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            throw new KeyNotFoundException("Service not found");
        }

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }
}
