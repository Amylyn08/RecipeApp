namespace RecipeApp.Searcher;

using RecipeApp.Models;

public class SearchByPriceRange: ISearcher{

    private double minPrice;
    private double maxPrice;

    /// <summary>
    /// Constructor for SearchByPriceRange, takes in a min price and max price and sets them.
    /// </summary>
    /// <param name="min">The min price.</param>
    /// <param name="max">The max price.</param>
    public SearchByPriceRange(double min, double max){
         minPrice = min;
         maxPrice = max;
    }

    /// <summary>
    /// Gets list of recipes where the price is in between (including) the range given.
    /// </summary>
    /// <param name="recipes">The list of recipes being iterated through.</param>
    /// <returns>The list of filtered recipes corresponding to the range of price.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            foreach(Ingredient ing in ingredientsOfRecipe(r)){
                if(ing.Price >= minPrice && ing.Price <= maxPrice){
                    filteredRecipes.Add(r);
                }
            }
        }
        return filteredRecipes;
    }

    /// <summary>
    /// Getss list of ingredients from a recipe
    /// </summary>
    /// <param name="r">The recipe being used</param>
    /// <returns>The list of ingredients of recipe.</returns>
    private static List<Ingredient> ingredientsOfRecipe(Recipe r){
        return r.Ingredients;
    }
}