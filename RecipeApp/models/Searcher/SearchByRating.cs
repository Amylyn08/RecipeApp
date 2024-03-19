namespace RecipeApp.Models;

public class SearchByRating{

    private int __criteria;

    /// <summary>
    /// Constructor taking assigning the criteria, corresponding to the num ratings specified.
    /// </summary>
    /// <param name="criteria">Rating/stars</param>
    public SearchByRating(int criteria){
        __criteria = criteria;
    }

    /// <summary>
    /// Gets all recipes with the corresponding rating specified.
    /// </summary>
    /// <param name="recipes">The list of recipes being used</param>
    /// <returns>filtered recipes containing specified ratings.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new List<Recipe>();
        foreach(Recipe r in recipes){
            foreach(Rating rating in ratingsOfRecipe(r)){
                if(rating.__stars == __criteria){
                    filteredRecipes.Add(r);
                }
            }
        }
        return filteredRecipes;
    }

    private static List<Rating> ratingsOfRecipe(Recipe r){
        return r.Ratings;
    }

}