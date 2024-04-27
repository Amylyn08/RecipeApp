namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

public class SearchByTime : SearcherBase{
    private readonly int  _minTime;
    private readonly int _maxTime;

    /// <summary>
    /// Constructor for SearchByTime, takes in a min time and max time, sets them.
    /// </summary>
    /// <param name="min">The min time.</param>
    /// <param name="max">The max time</param>
    public SearchByTime(SplankContext context, int min, int max) :base(context) {
        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minTime = min;
        _maxTime = max;
    }

    /// <summary>
    /// Gets list of recipes where time is in between min time and 
    ///max time.
    /// </summary>
    /// <returns>The filtered list of recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .Where(recipe => recipe.GetTimeToCook() >= _minTime && recipe.GetTimeToCook() <= _maxTime)
            .ToList();
        return filteredRecipes;
    }
}