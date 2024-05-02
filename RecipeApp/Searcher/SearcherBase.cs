namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public abstract class SearcherBase{
    public SplankContext Context { get; set; } = SplankContext.GetInstance();

    public SearcherBase(SplankContext context){
        Context = context;
    }
    /// <summary>
    /// Returns a list of recipes matching a filter.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    public abstract List<Recipe> FilterRecipes();

    
}