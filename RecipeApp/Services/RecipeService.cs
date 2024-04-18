using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeApp.Services;

public class RecipeService : ServiceBase {

    SplankContext splank = new SplankContext();
    public void CreateRecipe(Recipe recipe) {
        if (recipe == null) 
            throw new ArgumentException("Recipe cannot or user cannot be null");
        splank.Add(recipe);
        splank.SaveChanges();
    }

    public void DeleteRecipe(Recipe recipeToDelete, User user) {
        if (recipeToDelete == null || user == null)
            throw new ArgumentException("Recipe to delete is null or user is null");
        MockDatabase.AllRecipes.Remove(recipeToDelete);
        user.MadeRecipes.Remove(recipeToDelete);
    }

    public List<Recipe> SearchRecipes(SearcherBase searcher) {
        if (searcher == null) 
            throw new ArgumentException("Searcher cannot be null");
        return searcher.FilterRecipes();
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