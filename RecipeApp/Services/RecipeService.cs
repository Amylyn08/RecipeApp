using RecipeApp.Models;

namespace RecipeApp.Services;

public class RecipeService {
    public void CreateRecipe(Recipe recipe, User user) {
        if (recipe == null) 
            throw new ArgumentException("Recipe cannot be null");
        MockDatabase.AllRecipes.Add(recipe);
        foreach (User mockUser in MockDatabase.Users) {
            if (mockUser.Equals(user)) {
                mockUser.MadeRecipes.Add(recipe);
            }
        }
    }

    public void DeleteRecipe(int recipeId) {
        throw new NotImplementedException();
    }

    public Recipe GetRecipeById(int recipeId) {
        throw new NotImplementedException();
    }

    public List<Recipe> GetRecipesByUserId(int userId) {
        throw new NotImplementedException();
    }

    public void UpdateRecipe(Recipe updatedRecipe, User user) {
        if (updatedRecipe == null || user == null) 
            throw new ArgumentException("Updated recipe or user cannot be null");
        foreach (User mockUser in MockDatabase.Users) {
            if (mockUser.Equals(user)) {
                for (int i = 0; i < mockUser.MadeRecipes.Count; i++) {
                    if (user.MadeRecipes[i] == updatedRecipe) {
                        user.MadeRecipes[i] = updatedRecipe;
                        break;
                    }
                }
            }
        }
    }
}