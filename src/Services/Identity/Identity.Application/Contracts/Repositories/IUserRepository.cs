namespace Identity.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<int> CreateAsync(User user);
    Task ConfirmAccountAsync(User user);
    Task<User> FindUserByVerificationTokenAsync(string tokenKey);
    Task<User> FindUserByResetTokenAsync(string tokenKey);
    Task<User> FindUserByTokenAsync(string tokenKey);
    Task<User?> FindByEmailAsync(string email);
    Task UpdateAsync(User user); 
    Task AddToRoleAsync(User user);
    Task<List<string>> GetUserRolesAsync(User user);
    Task ClearResetTokenAsync(User user);
    Task ClearTokens();
}