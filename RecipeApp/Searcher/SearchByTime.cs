namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByTime : SearcherBase{

    private readonly int _criteria;

    /// <summary>
    /// Constructor for search by time takes in context and time with validation for greater than 0.
    /// </summary>
    /// <param name="context">The Context of application</param>
    /// <param name="time">The time desired by user.</param>
    /// <exception cref="ArgumentException">If time entered was less than 0</exception>
    public SearchByTime(SplankContext context, int time) :base(context) {
        if (time < 0)
            throw new ArgumentException("Time must be greater than 0.");
        _criteria = time;
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
            .Where(recipe => recipe.TotalTime >= _criteria - 5 && recipe.TotalTime <= _criteria + 5)
            .Select(recipe => recipe.Recipe)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Ratings)
            .Include(recipe => recipe.Tags)
            .ToList();
        return filteredRecipes;
    }
}