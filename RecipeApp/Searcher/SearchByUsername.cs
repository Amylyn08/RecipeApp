namespace RecipeApp.Searcher;

using RecipeApp.Models;


public class SearchByUsername : ISearcher{

    private string __criteria;

    /// <summary>
    /// Constructor for SearchByUsername takes in username
    /// </summary>
    /// <param name="username">Name of user</param>
    public SearchByUsername(string username){
        __criteria  = username;
    }

    /// <summary>
    /// Gets filtered list of recipes containing username 
    /// </summary>
    /// <param name="recipes">List of recipes</param>
    /// <returns>Filtered list of recipes containing the username/criteria</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if(r.User.Name.ToLower().Contains(__criteria.ToLower())){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }



}