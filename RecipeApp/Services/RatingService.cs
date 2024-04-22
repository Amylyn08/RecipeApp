using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Services;
 

public class RatingService : ServiceBase {
    public RatingService(SplankContext context) : base(context)
    {
    }

    /// <summary>
    /// Adds a rating to the recipe and updates the DB
    /// </summary>
    /// <param name="rating">The new recipe rating</param>
    /// <param name="recipe">The recipe recieving the rating</param>
    public void RatingRecipe(Rating rating, Recipe recipe) {
        if (rating == null || recipe == null) 
            throw new ArgumentException("Rating cannot be null");
        recipe.Ratings.Add(rating);
        Context.Update(recipe);
        Context.SaveChanges();
    }

}