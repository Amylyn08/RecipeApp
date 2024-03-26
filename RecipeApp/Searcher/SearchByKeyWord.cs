namespace RecipeApp.Searcher;

using RecipeApp.Models;

public class SearchKeyWord : ISearcher{
    
    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchKeyword, takes keyword.
    /// </summary>
    /// <param name="keyword">keyword wanna look for in desc.</param>
    public SearchKeyWord(string keyword){
        if (keyword == null)
            throw new ArgumentException("Keyword cannot be null");
        if (keyword.Length == 0) 
            throw new ArgumentException("Key cannot be empty");
        _criteria = keyword;
    }


    /// <summary>
    /// Gets list of recipes that contains a keyword from the description.
    /// </summary>
    /// <param name="recipes">List of recipes that is being searched</param>
    /// <returns>Filtered list of recipes that has keyuword in desc.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if(r.Description.Contains(_criteria, StringComparison.OrdinalIgnoreCase)){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }
}