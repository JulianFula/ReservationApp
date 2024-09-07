namespace ApiRest.Services.User;

public interface IUserService
{
    Task<IEnumerable<Models.User>> GetUsersAsync();
    Task<Models.User> GetUserByIdAsync(int id);
    Task<Models.User> CreateUserAsync(Models.User user);
    Task UpdateUserAsync(int id, Models.User user);
    Task DeleteUserAsync(int id);
}
