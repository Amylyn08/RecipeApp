namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByIngredients : SearcherBase
{

    private readonly string _criteria;

    /// <summary>
    /// Constructor contains the criteria, corresponding to the crtieria specified.
    /// </summary>
    /// <param name="ingredientName">Ingredient name  </param>
    public SearchByIngredients(string ingredientName)
    {
        if (ingredientName == null)
            throw new ArgumentException("Ingredient name cannot be null");
        if (ingredientName.Length == 0)
            throw new ArgumentException("Ingredient name cannot be empty");
        _criteria = ingredientName;
    }

    /// <summary>
    /// Searches through recipes list of context, gets the ingredients
    ///and sees if the name matches the string in the criteria
    /// </summary>
    /// <returns>The filtered list of recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        var recipes = base.Context.Recipes
                    .Where(recipe => recipe.Ingredients.Any(ingredient =>
                     ingredient.Name.Contains(_criteria) ))
                    .ToList<Recipe>();

        return recipes;
    }
}