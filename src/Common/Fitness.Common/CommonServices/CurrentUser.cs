using System.Security.Claims;

namespace Fitness.Common.CommonServices;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private ClaimsPrincipal? Claims => _httpContextAccessor?.HttpContext?.User;
    public bool IsInRole(string roleName)
    {
        var resultRoles = Claims != null ? null : Claims?.Claims.Where(_ => _.Type == ClaimTypes.Role).ToList();
        var response = resultRoles?.Any(_ => _.Value == roleName);
        return response ?? false;
    }

    public int UserId
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
            if (result == null)
            {
                return default;
            }

            return int.TryParse(result, out var identifier) ? identifier : default;
        }
    }
    
    public string? TrainerCode
    {
        get
        {
           return Claims?.Claims.FirstOrDefault(_ => _.Type == CommonConstants.ClaimTrainerType)?.Value; 
        }
    }
}