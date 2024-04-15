namespace RecipeApp.Searcher;

using RecipeApp.Models;

public class SearchByPriceRange: SearcherBase{

    private readonly double _minPrice;
    private readonly double _maxPrice;

    /// <summary>
    /// Constructor for SearchByPriceRange, takes in a min price and max price and sets them.
    /// </summary>
    /// <param name="min">The min price.</param>
    /// <param name="max">The max price.</param>
    public SearchByPriceRange(double min, double max){
        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minPrice = min;
        _maxPrice = max;
    }

    /// <summary>
    /// Gets list of recipes where the price is in between (including) the range given.
    /// </summary>
    /// <param name="recipes">The list of recipes being iterated through.</param>
    /// <returns>The list of filtered recipes corresponding to the range of price.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            if (r.GetTotalPrice() >= _minPrice && r.GetTotalPrice() <= _maxPrice) {
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }
}