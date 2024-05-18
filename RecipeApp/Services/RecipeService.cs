using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeApp.Services;

public class RecipeService : ServiceBase {
    public RecipeService(SplankContext context) : base(context)
    {
    }

    ///
    /// Gets some recipes from the DB
    public List<Recipe> GetSomeRecipes(int take, int skip) {
        return Context.Recipes
        .Include(r => r.Ingredients)
        .Include(r => r.Steps)
        .Include(r => r.Tags)
        .Include(r => r.Ratings)
        .Include(r => r.User)
        .Skip(take)
        .Take(skip)
        .ToList();
    }

    /// <summary>
    /// Adds recipe to DB
    /// </summary>
    /// <param name="recipeToAdd">The recipe being added to the DB</param>
    /// <exception cref="ArgumentException"></exception>
    public void CreateRecipe(Recipe recipeToAdd) {
        if (recipeToAdd == null)
            throw new ArgumentException("Recipe cannot be null");
        Context.Add(recipeToAdd);
        Context.SaveChanges();
    }

    /// <summary>
    /// Deletes a recipe from the DB
    ///</summary>
    ///<param name="recipeToDelete">The recipe being deleted</param>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Updates a recipe in the DB
    ///</summary>
    ///<param name="oldRecipe">The recipe being updated</param>
    ///<param name="newRecipe">The recipe that the old one is being updated to</param>
    /// <exception cref="ArgumentException"></exception>
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