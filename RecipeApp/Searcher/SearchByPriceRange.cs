namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByPriceRange: SearcherBase{

    private readonly double _minPrice;
    private readonly double _maxPrice;

    /// <summary>
    /// Constructor for SearchByPriceRange, takes in a min price and max price and sets them.
    /// </summary>
    /// <param name="min">The min price.</param>
    /// <param name="max">The max price.</param>
    public SearchByPriceRange(SplankContext context, double min, double max) : base(context){

        if (min < 0 || max < 0)
            throw new ArgumentException("Min or max cannot be negative");
        if (min > max) 
            throw new ArgumentException("Min cannot be greater than max");
        _minPrice = min;
        _maxPrice = max;
    }

    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
            .GroupJoin(Context.Ingredients,
                        recipe => recipe.RecipeId,
                        ingredient => ingredient.RecipeId,
                        (recipe, ingredients) => new
                        {
                            Recipe = recipe,
                            TotalPrice = ingredients.Sum(ing => ing.Price)
                        })
            .Where(recipe => recipe.TotalPrice >= _minPrice && recipe.TotalPrice <= _maxPrice)
            .Select(recipe => recipe.Recipe)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Ratings)
            .Include(recipe => recipe.Tags)
            .ToList();
        return filteredRecipes;
    }
}