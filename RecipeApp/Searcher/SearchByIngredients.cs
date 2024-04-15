namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByIngredients : SearcherBase{

    private readonly string _criteria;

    /// <summary>
    /// Constructor contains the criteria, corresponding to the crtieria specified.
    /// </summary>
    /// <param name="ingredientName">Ingredient name  </param>
    public SearchByIngredients(SplankContext context, string ingredientName) : base(context) { 
        if (ingredientName == null)
            throw new ArgumentException("Ingredient name cannot be null");
        if (ingredientName.Length == 0)
            throw new ArgumentException("Ingredient name cannot be empty");
        _criteria = ingredientName;
    }

    public override List<Recipe> FilterRecipes() {
        throw new NotImplementedException();
    }
}