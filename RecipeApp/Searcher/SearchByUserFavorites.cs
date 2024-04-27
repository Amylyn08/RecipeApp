using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Searcher;

public class SearchByUserFavorite : SearcherBase
{

    private User _user;

    /// <summary>
    /// Constructor for user, checking if null first.
    /// </summary>
    /// <param name="user">The user object of this searcher close</param>
    /// <exception cref="ArgumentNullException">If the user input was null.</exception>
    public SearchByUserFavorite(SplankContext context, User user) : base(context){
        if (user == null){
            throw new ArgumentNullException("user cannot be null");
        }
        _user = user;
    }

    /// <summary>
    /// Get filtered list of recipes using favorites list.
    /// Comparing the users from the list from the _user, if so, take recipes and
    /// makes list.
    /// </summary>
    /// <returns>The list of recipes from user's favorites.</returns>
    public override List<Recipe> FilterRecipes(){
        List<Recipe> filteredRecipes = Context.Favourites
            .Where(favorite => favorite.User.Equals(_user))
            .Select(favorite => favorite.Recipe)
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.Tags)
            .Include(recipe => recipe.Ratings)
            .ToList();
        return filteredRecipes;
    }

}