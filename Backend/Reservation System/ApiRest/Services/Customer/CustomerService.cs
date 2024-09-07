using ApiRest.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Services.Customer;

public class CustomerService : ICustomerService
{
    //Se crean variables para el guardado de la configuracion y el uso del Contexto de la base de datos
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    //Se crea un constructor para retornar la informacion en las variables
    public CustomerService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Models.Customer>> GetCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Models.Customer> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Models.Customer> CreateCustomerAsync(Models.Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(int id, Models.Customer customer)
    {
        if (id != customer.CustomerId)
        {
            throw new ArgumentException("ID mismatch");
        }

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found");
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
