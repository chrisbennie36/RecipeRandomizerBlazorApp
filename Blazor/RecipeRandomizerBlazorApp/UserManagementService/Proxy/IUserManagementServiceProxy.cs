using UserManagementService.Api.Client;

namespace RecipeRandomizerBlazorApp.UserManagementService.Proxy;

public interface IUserManagementServiceProxy
{
    Task CreateUserAsync(UserDto dto);
}
