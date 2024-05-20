namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

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
            .GroupJoin(Context.Steps,
                        recipe => recipe.RecipeId,
                        step => step.RecipeId,
                        (recipe, steps) => new
                        {
                            Recipe = recipe,
                            TotalTime = steps.Sum(ing => ing.TimeInMinutes)
                        })
            .Where(recipe => recipe.TotalTime >= _minTime && recipe.TotalTime <= _maxTime)
            .Select(recipe => recipe.Recipe)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Ratings)
            .Include(recipe => recipe.Tags)
            .ToList();
        return filteredRecipes;
    }
}