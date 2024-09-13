using RecipeApp.Models;

namespace RecipeApp.Api;

/// <summary>
/// Provides an interface for our api to have API's 
/// about recipe related information
/// </summary>
public interface IApiForIngredients {
    /// <summary>
    /// Fetches information about a recipe through an API
    /// </summary>
    /// <param name="recipe">Recipe that we want information on</param>
    /// <returns>An object mapped to the api's JSON response</returns>
    public ApiResponse Fetch(Recipe recipe);
}