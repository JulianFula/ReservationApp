namespace ApiRest.Services.Service;

public interface IServiceService
{
    Task<IEnumerable<Models.Service>> GetServicesAsync();
    Task<Models.Service> GetServiceByIdAsync(int id);
    Task<Models.Service> CreateServiceAsync(Models.Service service);
    Task UpdateServiceAsync(int id, Models.Service service);
    Task DeleteServiceAsync(int id);
}

