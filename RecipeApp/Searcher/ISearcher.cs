namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

public abstract class SearcherBase{
    public Context Context { get; set; }

    public ISearcher(Context context) {
        Context = context;
    }

    /// <summary>
    /// Returns a list of recipes matching a filter.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    abstract List<Recipe> FilterRecipes();
}