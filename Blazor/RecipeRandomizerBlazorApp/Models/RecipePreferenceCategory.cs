namespace RecipeRandomizerBlazorApp.Models;

using Category = Enums.RecipePreferenceCategory; 

public class RecipePreferenceCategory
{
    public Category Category { get; set; }
    public string ImagePath { get; set; } = string.Empty;
}
