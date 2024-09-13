namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

/// <summary>
/// Base class for searchers to have a splank context
/// </summary>
public abstract class SearcherBase{
    public SplankContext Context { get; set; } = SplankContext.GetInstance();

    /// <summary>
    /// Constructs a new instance of SearcherBase
    /// </summary>
    /// <param name="context">Connection to db</param>
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