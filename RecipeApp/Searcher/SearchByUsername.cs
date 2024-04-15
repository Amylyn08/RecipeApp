namespace RecipeApp.Searcher;

using RecipeApp.Models;


public class SearchByUsername : SearcherBase{

    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchByUsername takes in username
    /// </summary>
    /// <param name="username">Name of user</param>
    public SearchByUsername(string username) {
        if (username == null)
            throw new ArgumentException("Username cannot be null");
        if (username.Length == 0)
            throw new ArgumentException("Username cannot be empty");
        _criteria = username;
    }

    /// <summary>
    /// Gets filtered list of recipes containing username 
    /// </summary>
    /// <param name="recipes">List of recipes</param>
    /// <returns>Filtered list of recipes containing the username/criteria</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if(r.User.Name.Contains(_criteria, StringComparison.OrdinalIgnoreCase)){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }



}