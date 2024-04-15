namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

public abstract class SearcherBase{
    public DbContext Context { get; set; }

    public SearcherBase(DbContext context) {
        if (context == null) 
            throw new ArgumentNullException("Context is null");
        Context = context;
    }

    /// <summary>
    /// Returns a list of recipes matching a filter.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    public abstract List<Recipe> FilterRecipes();
}