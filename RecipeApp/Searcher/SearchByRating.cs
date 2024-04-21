namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

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
        .Where(recipe => recipe.Ratings.Any(rating => rating.Stars == _criteria))
        .ToList<Recipe>();

        return filteredRecipes;
    }
}