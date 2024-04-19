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
    /// <param name="rating">The rating that the recipe gonna get</param>
    public void RatingRecipe(Recipe recipeToRate, Rating rating) {
        recipeToRate.Ratings.Add(rating);
    }

    /// <summary>
    /// Creates a rating
    /// </summary>
    /// <param name="currentUser">The user thats creating the rating</param>
    /// <param name="stars">Num of stars of rating</param>
    /// <param name="description">Comment that user adds</param>
    /// <returns></returns>
    public Rating CreateRating(User currentUser, int stars, string description) {
        return new Rating(stars, description, currentUser);
    }
}