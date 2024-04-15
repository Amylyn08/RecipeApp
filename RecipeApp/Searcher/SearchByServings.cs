using Microsoft.EntityFrameworkCore;
using RecipeApp.Searcher;

namespace RecipeApp.Models;

public class SearchByServings : SearcherBase{

    private readonly int _criteria;

    /// <summary>
    /// Constructor for class, takes in number of servings specified for search.
    /// </summary>
    /// <param name="servings">The number of servings specified</param>
    public SearchByServings(DbContext context, int servings) : base(context) {
        if (servings < Constants.MIN_SERVINGS) 
            throw new ArgumentException($"Servings must be atleast ${Constants.MIN_SERVINGS}");
        _criteria = servings;
    }

    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}