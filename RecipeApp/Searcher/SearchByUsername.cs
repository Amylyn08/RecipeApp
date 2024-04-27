namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;


public class SearchByUsername : SearcherBase{

    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchByUsername takes in username
    /// </summary>
    /// <param name="username">Name of user</param>
    public SearchByUsername(SplankContext context, string username) : base(context){
        if (username == null)
            throw new ArgumentException("Username cannot be null");
        if (username.Length == 0)
            throw new ArgumentException("Username cannot be empty");
        _criteria = username;
    }

    /// <summary>
    /// Gets list of users where recipe was made by user with specified username.
    /// </summary>
    /// <returns>The filtered recipes.</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .Where(recipe => recipe.User.Name.Equals(_criteria))
            .ToList();
        return filteredRecipes;
    }
}