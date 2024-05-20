namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByPriceRange: SearcherBase {

    private readonly double _price;
    
    /// <summary>
    /// Constructor for SearchByPriceRange, takes in a min price and max price and sets them.
    /// </summary>
    /// <param name="min">The min price.</param>
    /// <param name="max">The max price.</param>
    public SearchByPriceRange(SplankContext context, double price) : base(context){

        if (price < 0)
            throw new ArgumentException("Price cannot be negative");
        _price = price;
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
            .Where(recipe => recipe.TotalPrice >= _price - 5 && recipe.TotalPrice <= _price + 5)
            .Select(recipe => recipe.Recipe)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Ratings)
            .Include(recipe => recipe.Tags)
            .ToList();
        return filteredRecipes;
    }

}