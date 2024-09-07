namespace ApiRest.Services.Customer;

public interface ICustomerService
{
    Task<IEnumerable<Models.Customer>> GetCustomersAsync();
    Task<Models.Customer> GetCustomerByIdAsync(int id);
    Task<Models.Customer> CreateCustomerAsync(Models.Customer customer);
    Task UpdateCustomerAsync(int id, Models.Customer customer);
    Task DeleteCustomerAsync(int id);
}
