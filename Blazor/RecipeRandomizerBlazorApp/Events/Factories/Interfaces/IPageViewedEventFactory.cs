using Utilities.RecipeRandomizer.Events;

namespace RecipeRandomizerBlazorApp.Events.Factories.Interfaces;

public interface IPageViewedEventFactory
{
    public PageViewed GetPreferencesPageViewedEvent(Enums.RecipePreferenceCategory recipePreferenceCategory);
}
