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
    /// Searches list of recipes of context and returns those that includes the name.
    /// Using LINQ quering
    /// </summary>
    /// <returns>List of filtered recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> recipeList = (from recipe in Context.Recipes
                                   where recipe.Ingredients.Any(ing => ing.Name.Contains(_criteria))
                                   select recipe).ToList<Recipe>();
        return recipeList;

    }
}