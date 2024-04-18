namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public abstract class SearcherBase{
    public SplankContext Context { get; set; } = new SplankContext();

    /// <summary>
    /// Returns a list of recipes matching a filter.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    public abstract List<Recipe> FilterRecipes();
}