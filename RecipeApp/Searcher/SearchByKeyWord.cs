namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public class SearchKeyWord : SearcherBase {
    
    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchKeyword, takes keyword.
    /// </summary>
    /// <param name="keyword">keyword wanna look for in desc.</param>
    public SearchKeyWord(string keyword) {
        if (keyword == null)
            throw new ArgumentException("Keyword cannot be null");
        if (keyword.Length == 0) 
            throw new ArgumentException("Key cannot be empty");
        _criteria = keyword;
    }
    /// <summary>
    /// Searches through recipes list of context, gets the ingredients
    ///and sees if the name of recipe matches the string in criteria/keyword.
    /// </summary>
    /// <returns>The filtered list of recipes</returns>
    public override List<Recipe> FilterRecipes()
    {
        // var filteredRecipes = Context.Recipes
        //                     .Where(recipe => recipe.Name.Any(name => 
        //                     name.Contains(_criteria)));

        // return filteredRecipes;
        throw new NotImplementedException();
    }
}