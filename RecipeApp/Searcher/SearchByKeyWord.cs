namespace RecipeApp.Searcher;

using System.Diagnostics;
using RecipeApp.Models;

public class SearchKeyWord : ISearcher{
    
    private string __criteria;

    /// <summary>
    /// Constructor for SearchKeyword, takes keyword.
    /// </summary>
    /// <param name="keyword">keyword wanna look for in desc.</param>
    public SearchKeyWord(string keyword){
        __criteria = keyword;
    }


    /// <summary>
    /// Gets list of recipes that contains a keyword from the description.
    /// </summary>
    /// <param name="recipes">List of recipes that is being searched</param>
    /// <returns>Filtered list of recipes that has keyuword in desc.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if(r.Description.ToLower().Contains(__criteria.ToLower())){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }
}