using Microsoft.EntityFrameworkCore;
using recipeapp;
using RecipeApp.Context;
using RecipeApp.Searcher;

namespace RecipeApp.Models;

public class SearchByServings : SearcherBase{

    private readonly int _criteria;

    /// <summary>
    /// Constructor for class, takes in number of servings specified for search.
    /// </summary>
    /// <param name="servings">The number of servings specified</param>
    public SearchByServings(SplankContext content, int servings) : base(content) {
        if (servings < Constants.MIN_SERVINGS) 
            throw new ArgumentException($"Servings must be atleast ${Constants.MIN_SERVINGS}");
        _criteria = servings;
    }

    /// <summary>
    /// Gets filtered list depending on the amount of servings matching
    /// the criteria specified
    /// </summary>
    /// <returns>The filtered list of recipes.</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
                                    .Where(recipe => recipe.Servings == _criteria)
                                    .ToList<Recipe>();

        return filteredRecipes;
    }
}