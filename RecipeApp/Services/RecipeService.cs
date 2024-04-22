using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeApp.Services;

public class RecipeService : ServiceBase {
    public RecipeService(SplankContext context) : base(context)
    {
    }

    /// <summary>
    /// Cre
    /// </summary>
    /// <param name="recipeToAdd"></param>
    /// <exception cref="ArgumentException"></exception>
    public void CreateRecipe(Recipe recipeToAdd) {
        if (recipeToAdd == null)
            throw new ArgumentException("Recipe cannot be null");
        Context.Add(recipeToAdd);
        Context.SaveChanges();
    }

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

    public void UpdateRecipe(Recipe oldRecipe, Recipe updatedRecipe) {
        if (updatedRecipe == null || oldRecipe == null)
            throw new ArgumentException("Updated recipe cannot be null");


        oldRecipe.Description = updatedRecipe.Description;
        oldRecipe.Servings = updatedRecipe.Servings;
        oldRecipe.Ingredients = updatedRecipe.Ingredients;
        oldRecipe.Steps = updatedRecipe.Steps;
        oldRecipe.Tags = updatedRecipe.Tags;
        oldRecipe.Name = updatedRecipe.Name;
        oldRecipe.Ratings = updatedRecipe.Ratings;

        Context.Update(oldRecipe);
        Context.SaveChanges();

    }
}