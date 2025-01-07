using MassTransit;
using Microsoft.AspNetCore.Components;
using RecipeRandomizerBlazorApp.Dtos;
using Utilities.RecipeRandomizer.Events;

namespace RecipeRandomizerBlazorApp.Components.Pages.RateRecipePage;

public partial class RateRecipe : ComponentBase
{
    [Inject]
    public IPublishEndpoint PublishEndpoint { get; set; }

    [Inject] 
    public StateContainer.StateContainer StateContainer { get; set; }

    [Parameter]
    public int DtoHashCode { get; set; }

    [SupplyParameterFromForm]
    public string RecipeRating { get; set; }

    public RecipeRatingDto RecipeRatingDto { get; set; } 

    protected override Task OnInitializedAsync()
    {
        if(StateContainer.ObjectTunnel.ContainsKey(DtoHashCode))
        {
            RecipeRatingDto = (RecipeRatingDto) StateContainer.ObjectTunnel[DtoHashCode];
        }

        return base.OnInitializedAsync();
    }

    protected void RateRecipeButton_Clicked()
    {
        if(RecipeRatingDto == null)
        {
            return;
        }

        PublishEndpoint.Publish(new RecipeRated
        {
            UserId = RecipeRatingDto.UserId,
            RecipeName = RecipeRatingDto.RecipeName,
            RecipeUrl = RecipeRatingDto.RecipeUrl.ToString(),
            RecipeRating = int.Parse(RecipeRating)
        });
    }
}
