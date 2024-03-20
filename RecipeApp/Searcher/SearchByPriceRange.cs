namespace RecipeApp.Searcher;

using RecipeApp.Models;

public class SearchByPriceRange: ISearcher{

    private double minPrice;
    private double maxPrice;

    public SearchByPriceRange(double min, double max){
         minPrice = min;
         maxPrice = max;
    }

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