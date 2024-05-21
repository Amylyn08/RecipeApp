namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

/// <summary>
/// Searches recipes based on ratings
/// </summary>
public class SearchByRating : SearcherBase{
    private readonly int _criteria;

    /// <summary>
    /// Constructor taking assigning the criteria, corresponding to the num ratings specified.
    /// </summary>
    /// <param name="criteria">Rating/stars</param>
    public SearchByRating(SplankContext content, int rating) : base(content) {
        if (rating < 0 || rating > 5) 
            throw new ArgumentException("Rating must be between 1 and 5");
        _criteria = rating;
    }

    /// <summary>
    /// Gets list of recipes where the stars of rating matches criteria specified.
    /// </summary>
    /// <returns>The filtered list</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .Where(recipe => recipe.AverageRating >= _criteria - 0.5 && recipe.AverageRating <= _criteria + 0.5)
            .ToList();
        return filteredRecipes;
    }
}