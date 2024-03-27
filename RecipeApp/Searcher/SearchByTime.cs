namespace RecipeAppTest.searcher;

using RecipeApp.Models;
using RecipeApp.Searcher;

public class SearchByTime : ISearcher{
    private readonly int  _minTime;
    private readonly int _maxTime;

    /// <summary>
    /// Constructor for SearchByTime, takes in a min time and max time, sets them.
    /// </summary>
    /// <param name="min">The min time.</param>
    /// <param name="max">The max time</param>
    public SearchByTime(int min, int max) {
        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minTime = min;
        _maxTime = max;
    }

    /// <summary>
    /// Gets filtered list of recipes corresponding to the range, if they time is between the range given.
    /// </summary>
    /// <param name="recipes">List of recipes being iterated through.</param>
    /// <returns>The list of recipes filtered.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if (r.GetTimeToCook() >= _minTime && r.GetTimeToCook() <= _maxTime) {
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }

    /// <summary>
    /// Gets list of steps for a recipe
    /// </summary>
    /// <param name="r">The recipe being used</param>
    /// <returns>List of steps for recipe</returns>
    private List<Step> StepsInRecipe (Recipe r){
        return r.Steps;
    }

}