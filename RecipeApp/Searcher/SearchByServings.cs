using RecipeApp.Searcher;

namespace RecipeApp.Models;

public class SearchByServings : ISearcher{

    private int __criteria;

    /// <summary>
    /// Constructor for class, takes in number of servings specified for search.
    /// </summary>
    /// <param name="servings">The number of servings specified</param>
    public SearchByServings(int servings){
        __criteria = servings;
    }

    /// <summary>
    /// Gets list of recipes containing the number of servings specified.
    /// </summary>
    /// <param name="recipes">The list of recipes being used.</param>
    /// <returns>Returns list of filtered recipes containing the servings.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new List<Recipe>();
        foreach(Recipe r in recipes){
            if(r.Servings == __criteria){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }
}