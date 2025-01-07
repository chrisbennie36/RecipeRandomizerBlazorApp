using RecipeRandomizerBlazorApp.Enums;
using RecipeRandomizerBlazorApp.Events.Factories.Interfaces;
using RecipeRandomizerBlazorApp.Exceptions;
using Utilities.RecipeRandomizer.Events;

namespace RecipeRandomizerBlazorApp.Events.Factories;

public class PageViewedEventFactory : IPageViewedEventFactory
{
    public PageViewed GetPreferencesPageViewedEvent(RecipePreferenceCategory recipePreferenceCategory)
    {
        switch(recipePreferenceCategory)
        {
            case RecipePreferenceCategory.Meat:
                return new PageViewed
                {
                    PageType = PageType.MeatBasedPreferences
                };
            case RecipePreferenceCategory.Pescatarian:
                return new PageViewed
                {
                    PageType = PageType.PescatarianBasedPreferences
                };
            case RecipePreferenceCategory.Vegetarian:
                return new PageViewed
                {
                    PageType = PageType.VegetarianBasedPreferences
                };
            default:
                throw new PageTypeNotImplementedException($"Recipe Preference Category: {recipePreferenceCategory} does not have a corresponding {nameof(PageType)} mapping");
        }
    }
}
