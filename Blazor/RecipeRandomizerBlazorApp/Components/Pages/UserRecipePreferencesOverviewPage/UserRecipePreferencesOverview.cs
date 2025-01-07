using MassTransit;
using Microsoft.AspNetCore.Components;
using RecipeRandomizerBlazorApp.Models;

namespace RecipeRandomizerBlazorApp.Components.Pages.UserRecipePreferencesOverviewPage;

public partial class UserRecipePreferencesOverview : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public IEnumerable<RecipePreferenceCategory> UserRecipePreferenceCategories { get; set; } = Enumerable.Empty<RecipePreferenceCategory>();

    protected override Task OnInitializedAsync()
    {
        LoadUserRecipePreferences();
        return base.OnInitializedAsync();
    }

    protected void OpenRecipePreferencesForCategory(Enums.RecipePreferenceCategory selectedCategory)
    {
        NavigationManager.NavigateTo($"/user/recipePreferences/{(int)selectedCategory}");
    }

    private void LoadUserRecipePreferences()
    {
        //First, fetch the recipe preferences for the user, get these from the DB
        //var userRecipePreferences = blah

        //then, set the recipe preference categories to display based on these preferences
        SetUserRecipePreferenceCategories();
    }

    private void SetUserRecipePreferenceCategories()
    {
        RecipePreferenceCategory meatCategory = new RecipePreferenceCategory { Category = Enums.RecipePreferenceCategory.Meat, ImagePath = "../images/MeatCategory.jpg" };
        RecipePreferenceCategory vegetarianCategory = new RecipePreferenceCategory { Category = Enums.RecipePreferenceCategory.Vegetarian, ImagePath = "../images/VegetarianCategory.jpg" };
        RecipePreferenceCategory pescatarianCategory = new RecipePreferenceCategory { Category = Enums.RecipePreferenceCategory.Pescatarian, ImagePath = "../images/PescatarianCategory.jpg" };

        UserRecipePreferenceCategories = new List<RecipePreferenceCategory>
        {
            meatCategory, vegetarianCategory, pescatarianCategory
        };
    }
}
