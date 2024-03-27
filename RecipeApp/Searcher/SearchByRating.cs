namespace RecipeApp.Searcher;

using RecipeApp.Models;

public class SearchByRating : ISearcher{

    private readonly int _criteria;

    /// <summary>
    /// Constructor taking assigning the criteria, corresponding to the num ratings specified.
    /// </summary>
    /// <param name="criteria">Rating/stars</param>
    public SearchByRating(int rating){
        if (rating < 0 || rating > 5) 
            throw new ArgumentException("Rating must be between 1 and 5");
        _criteria = rating;
    }

    /// <summary>
    /// Gets all recipes with the corresponding rating specified.
    /// </summary>
    /// <param name="recipes">The list of recipes being used</param>
    /// <returns>filtered recipes containing specified ratings.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new List<Recipe>();
        foreach(Recipe r in recipes){
            foreach(Rating rating in RatingsOfRecipe(r)){
                if(rating.Stars == _criteria){
                    filteredRecipes.Add(r);
                }
            }
        }
        return filteredRecipes;
    }

    /// <summary>
    /// Gets list of ratings for a recipe
    /// </summary>
    /// <param name="r">The recipe specified</param>
    /// <returns>List of ratings of recipe.</returns>
    private static List<Rating> RatingsOfRecipe(Recipe r){
        return r.Ratings;
    }

}