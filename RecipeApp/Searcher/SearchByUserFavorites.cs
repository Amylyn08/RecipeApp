using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Searcher;

public class SearchByUserFavorite : SearcherBase
{

    private readonly User _user;

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
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        List<Recipe> filteredRecipes = Context.Favourites
                                    .Where(favorite => favorite.User == _user)
                                    .Select(favorite => favorite.Recipe)
                                    .ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        return filteredRecipes;
    }

}