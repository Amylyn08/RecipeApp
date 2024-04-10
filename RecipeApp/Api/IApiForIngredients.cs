using RecipeApp.Models;

namespace RecipeApp.Api;

public interface IApiForIngredients {
    public ApiResponse Fetch(Recipe recipe);
}