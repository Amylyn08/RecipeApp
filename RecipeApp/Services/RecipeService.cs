using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeApp.Services;

public class RecipeService : ServiceBase {
    public RecipeService(SplankContext context) : base(context)
    {
    }

    // public void CreateRecipe(Recipe recipe, User user) {
        // if (recipe == null || user == null) 
        //     throw new ArgumentException("Recipe cannot or user cannot be null");
        // MockDatabase.AllRecipes.Add(recipe);
        // foreach (User mockUser in MockDatabase.Users) {
        //     if (mockUser.Equals(user)) {
        //         mockUser.MadeRecipes.Add(recipe);
        //     }
        // }
    // }


    public void CreateRecipe(Recipe recipeToAdd) {
        if (recipeToAdd == null)
            throw new ArgumentException("Recipe cannot be null");
        Context.Add(recipeToAdd);
        Context.SaveChanges();
    }
    // public void DeleteRecipe(Recipe recipeToDelete, User user) {
        // if (recipeToDelete == null || user == null)
        //     throw new ArgumentException("Recipe to delete is null or user is null");
        // MockDatabase.AllRecipes.Remove(recipeToDelete);
        // user.MadeRecipes.Remove(recipeToDelete);
    // }

    public void DeleteRecipe(Recipe recipeToDelete) {
        if (recipeToDelete == null)
            throw new ArgumentException("Recipe cannot be null");
        Context.Remove(recipeToDelete);
        Context.SaveChanges();
    }

    public List<Recipe> SearchRecipes(SearcherBase searcher) {
        if (searcher == null) 
            throw new ArgumentException("Searcher cannot be null");
        return searcher.FilterRecipes();
    }

    // public void UpdateRecipe(Recipe updatedRecipe, User user) {
        // if (updatedRecipe == null || user == null) 
        //     throw new ArgumentException("Updated recipe or user cannot be null");
        // foreach (User mockUser in MockDatabase.Users) {
        //     if (mockUser.Equals(user)) {
        //         for (int i = 0; i < mockUser.MadeRecipes.Count; i++) {
        //             if (user.MadeRecipes[i] == updatedRecipe) {
        //                 user.MadeRecipes[i] = updatedRecipe;
        //                 break;
        //             }
        //         }
        //     }
        // }
    // }

    public void UpdateRecipe(Recipe oldRecipe, Recipe updatedRecipe) {
        if (updatedRecipe == null || oldRecipe == null)
            throw new ArgumentException("Updated recipe cannot be null");


        oldRecipe.Description = updatedRecipe.Description;
        oldRecipe.Servings = updatedRecipe.Servings;
        oldRecipe.Ingredients = updatedRecipe.Ingredients;
        oldRecipe.Steps = updatedRecipe.Steps;
        oldRecipe.Tags = updatedRecipe.Tags;
        oldRecipe.Name = updatedRecipe.Name;

        Context.Update(oldRecipe);
        Context.SaveChanges();

    }
}