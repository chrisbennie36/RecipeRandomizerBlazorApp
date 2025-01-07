using Microsoft.AspNetCore.Components;
using RecipeRandomizerBlazorApp.UserManagementService.Proxy;
using UserManagementService.Api.Client;

namespace RecipeRandomizerBlazorApp.Components.Pages.RegisterUserPage;

public partial class RegisterUser : ComponentBase
{
    [SupplyParameterFromForm]
    public string Username { get; set; }

    [SupplyParameterFromForm]
    public string Password { get; set; }

    [Inject]
    public IUserManagementServiceProxy UserManagementServiceProxy { get; set; }

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    protected void RegisterUserButton_Clicked()
    {
        UserDto userDto = new UserDto
        {
            Username = Username,
            Password = Password
        };

        UserManagementServiceProxy.CreateUserAsync(userDto);
    }
}
