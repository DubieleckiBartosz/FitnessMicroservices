namespace Identity.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<int> CreateAsync(User user);
    Task<User> FindUserByTokenAsync(string tokenKey);
    Task<User> FindByEmailAsync(string email, bool throwWhenNull = true);
    Task UpdateAsync(User user);
    Task AddToRoleAsync(User user);
    Task<List<string>> GetUserRolesAsync(User user);
    Task ClearTokens();
}