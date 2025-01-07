namespace RecipeRandomizerBlazorApp.Dtos;

public class RecipeRatingDto
{
    public int UserId { get; set;}
    public string RecipeName { get; set; } = string.Empty;
    public Uri RecipeUrl { get; set; } = new Uri("http://localhost");
}
