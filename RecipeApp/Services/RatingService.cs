using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Services;
 

public class RatingService : ServiceBase {
    public RatingService(SplankContext context) : base(context)
    {
    }

    /// <summary>
    /// Adds a rating to the recipe
    /// </summary>
    /// <param name="recipeToRate">The recipe getting a rating</param>
    // /// <param name="rating">The rating that the recipe gonna get</param>
    // public void RatingRecipe(Recipe recipeToRate, Rating rating) {
    //     recipeToRate.Ratings.Add(rating);
    // }


    public void RatingRecipe(Rating rating, Recipe recipe) {
        if (rating == null || recipe == null) 
            throw new ArgumentException("Rating cannot be null");
        recipe.Ratings.Add(rating);
        Context.Update(recipe);
        Context.SaveChanges();
    }
}