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

    /// <summary>
    /// Updates the rating from the rating from the recipes list in recipe.
    /// </summary>
    /// <param name="rating">The rating object of recipes.</param>
    /// <param name="recipe">The Recipe object to have the recipes updated</param>
    /// <param name="newDesc">The new description of the rating</param>
    /// <param name="newStars">The new stars of the rating</param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateRecipeRating(Rating rating, Recipe recipe, string newDesc, int newStars ){
        //get ratings from the recipe, recipe has list of ratings
        Rating? ratingToUpdate = recipe.Ratings.Find(r => r.RatingId == rating.RatingId) ?? 
            throw new ArgumentException("Rating cannot be found");
        ratingToUpdate.Stars = newStars;
        ratingToUpdate.Description = newDesc;
        Context.Update(ratingToUpdate);
        Context.SaveChanges();
    }

    /// <summary>
    /// Deletes the rating from the context
    /// </summary>
    /// <param name="rating">The rating object to be removed</param>
    /// <exception cref="ArgumentException">Exception if the rating is null</exception>
    public void DeleteRecipeRating(Rating rating){
        if (rating == null) 
            throw new ArgumentException("Rating cannot be null");
        Context.Remove(rating);
        Context.SaveChanges();
    }

}