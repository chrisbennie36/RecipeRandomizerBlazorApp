using MassTransit;
using Microsoft.AspNetCore.Components;
using Utilities.RecipeRandomizer.Events;
using RecipeRandomizerBlazorApp.Events.Factories.Interfaces;
using RecipeRandomizerBlazorApp.Exceptions;
using RecipeRandomizerBlazorApp.Models;
using RecipePreferenceCategory = RecipeRandomizerBlazorApp.Enums.RecipePreferenceCategory;

namespace RecipeRandomizerBlazorApp.Components.Pages.UserRecipePreferencesCategoryPage;

public partial class UserRecipePreferences : ComponentBase
{
    [Parameter]
    public string RecipePreferenceCategoryId { get; set; } = string.Empty;

    private readonly IPublishEndpoint publishEndpoint;
    private readonly IPageViewedEventFactory pageViewedEventFactory;

    public UserRecipePreferences(IPublishEndpoint publishEndpoint, IPageViewedEventFactory pageViewedEventFactory)
    {
        this.publishEndpoint = publishEndpoint;
        this.pageViewedEventFactory = pageViewedEventFactory;
    }

    public IEnumerable<RecipePreferenceModel> UserRecipePreferencesForCategory { get; set; } = Enumerable.Empty<RecipePreferenceModel>();

    protected override Task OnInitializedAsync()
    {
        SendPageViewedEvent();
        LoadUserRecipePreferences();
        return base.OnInitializedAsync();
    }

    private void SendPageViewedEvent()
    {
        RecipePreferenceCategory recipePreferenceCategory = (RecipePreferenceCategory)int.Parse(RecipePreferenceCategoryId);

        try
        {
            PageViewed pageViewedEvent = pageViewedEventFactory.GetPreferencesPageViewedEvent(recipePreferenceCategory);
            publishEndpoint.Publish(pageViewedEvent);
        }
        catch(PageTypeNotImplementedException e)
        {

        }
    }

    private void LoadUserRecipePreferences()
    {
        //First, fetch the recipe preferences for the user, get these from the DB
        //var userRecipePreferences = blah

        //then, filter the recipe preference by the relevant category
        SetUserRecipePreferencesForCategory();
    }

    private void SetUserRecipePreferencesForCategory()
    {
        switch(int.Parse(RecipePreferenceCategoryId))
        {
            case (int)Enums.RecipePreferenceCategory.Meat:
                RecipePreferenceModel chickenRecipePreference = new RecipePreferenceModel { Category = Enums.RecipePreferenceCategory.Meat, Name = "Chicken" };
                RecipePreferenceModel porkRecipePreference = new RecipePreferenceModel { Category = Enums.RecipePreferenceCategory.Meat, Name = "Pork" };

                UserRecipePreferencesForCategory = new List<RecipePreferenceModel> { chickenRecipePreference, porkRecipePreference };
                break;
            default:
                break; 
        }
    }
}
