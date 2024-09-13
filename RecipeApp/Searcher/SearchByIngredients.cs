namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

/// <summary>
/// Search for recipes by an ingredient
/// </summary>
public class SearchByIngredients : SearcherBase
{
    private readonly string _criteria;

    /// <summary>
    /// Constructor contains the criteria, corresponding to the crtieria specified.
    /// </summary>
    /// <param name="ingredientName">Ingredient name  </param>
    public SearchByIngredients(SplankContext context, string ingredientName) : base(context)
    {
        if (ingredientName == null)
            throw new ArgumentException("Ingredient name cannot be null");
        if (ingredientName.Length == 0)
            throw new ArgumentException("Ingredient name cannot be empty");
        _criteria = ingredientName.ToLower();

    }

    /// <summary>
    /// Searches through recipes list of context, gets the ingredients
    ///and sees if the name matches the string in the criteria
    /// </summary>
    /// <returns>The filtered list of recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        var recipes = Context.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .Where(recipe => recipe.Ingredients.Any(ingredient => ingredient.Name.Contains(_criteria)))
            .ToList();
        return recipes;
    }
}