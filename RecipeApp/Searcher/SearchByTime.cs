namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
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
    public SearchByTime(int min, int max) {
        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minTime = min;
        _maxTime = max;
    }

    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}