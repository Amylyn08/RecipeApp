namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

/// <summary>
/// Search for a recipe based on a keyword within the recipe description
/// </summary>
public class SearchKeyWord : SearcherBase {
    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchKeyword, takes keyword.
    /// </summary>
    /// <param name="keyword">keyword wanna look for in desc.</param>
    public SearchKeyWord(SplankContext context , string keyword): base(context){
        if (keyword == null)
            throw new ArgumentException("Keyword cannot be null");
        if (keyword.Length == 0) 
            throw new ArgumentException("Key cannot be empty");
        _criteria = keyword.ToLower();
    }
    /// <summary>
    /// Searches through recipes list of context, gets the ingredients
    ///and sees if the description of recipe contains the string in criteria/keyword.
    /// </summary>
    /// <returns>The filtered list of recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .Where(recipe => recipe.Description.Contains(_criteria))
            .ToList();
        return filteredRecipes;     
    }
}