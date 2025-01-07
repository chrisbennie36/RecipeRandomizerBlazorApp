using Microsoft.AspNetCore.Mvc;
using UserManagementService.Api.Client;
using Serilog;

namespace RecipeRandomizerBlazorApp.UserManagementService.Proxy;

public class UserManagementServiceProxy : IUserManagementServiceProxy
{
    private readonly UserClient userClient;

    public UserManagementServiceProxy(UserClient userClient)
    {
        this.userClient = userClient;
    }

    public async Task CreateUserAsync(UserDto dto)
    {
        try
        {
            await userClient.CreateUserAsync(dto);
        }
        catch(ApiException e)
        {
            Log.Error("Error when creating a new User: {errorMessage} {innerExceptionMessage}", e.Message, e.InnerException?.Message);
        }
    }
}
