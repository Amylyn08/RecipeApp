using RecipeApp.Models;

namespace RecipeApp.Services;

public interface IRecipeService {
    public List<Recipe> GetRecipesByUserId(int userId);
    public Recipe GetRecipeById(int recipeId);
    public void CreateRecipe(Recipe recipe);
    public void DeleteRecipe(int recipeId); 
    public void UpdateRecipe(int recipeId, Recipe updatedRecipe);
}