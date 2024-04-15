namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

public class SearchByPriceRange: SearcherBase{

    private readonly double _minPrice;
    private readonly double _maxPrice;

    /// <summary>
    /// Constructor for SearchByPriceRange, takes in a min price and max price and sets them.
    /// </summary>
    /// <param name="min">The min price.</param>
    /// <param name="max">The max price.</param>
    public SearchByPriceRange(DbContext context, double min, double max) : base(context) {
        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minPrice = min;
        _maxPrice = max;
    }

    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}