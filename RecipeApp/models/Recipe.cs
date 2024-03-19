namespace RecipeApp.Models;

using RecipeApp.Constants;

/// <summary>
/// Recipe schema
/// </summary>
public class Recipe {
    public User User { get; private set; }
    public string Description { get; private set; }
    public int Servings { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Step> Steps { get; private set; }
    public List<Rating> Ratings { get; private set; }
    public List<Tag> Tags { get; private set; }

    /// <summary>
    /// Constructor with user, ingredients, steps and ratings
    /// </summary>
    /// <param name="user">User who made the recipe</param>
    /// <param name="description">Short description of recipe</param>
    /// <param name="servings">Serving amount</param>
    /// <param name="ingredients">List of required ingredients</param>
    /// <param name="steps">Steps to complete the recipe</param>
    /// <param name="ratings">Recipe ratings</param>
    /// <param name="tags">List of tags</param>
    /// <exception cref="ArgumentException">If any fields are null, empty or doesn't respect certain constraints</exception>
    public Recipe(User user, string description, int servings, List<Ingredient> ingredients, 
            List<Step> steps, List<Rating> ratings, List<Tag> tags) {
        if (user == null) 
            throw new ArgumentException("User cannot be null");
        if (servings < Constants.MIN_SERVINGS) 
            throw new ArgumentException("Serving(s) must be greater than 0");
        if (ratings == null) 
            throw new ArgumentException("Ratings cannot be null");
        description ??= "";
        if (description.Length > Constants.MAX_DESCRIPTION_LENGTH) {
            throw new ArgumentException("Recipe description cannot exceed " + Constants.MAX_DESCRIPTION_LENGTH + " characters");
        }

        CheckIngredients(ingredients);
        CheckTags(tags);
        CheckSteps(steps);
        
        User = new(user.Name, user.Description, user.Password, user.Favorites);
        Description = description;
        Servings = servings;
        
        PopulateIngredients(ingredients);
        PopulateRatings(ratings);
        PopulateTags(tags);
        PopulateSteps(steps);
    }

    /// <summary>
    /// Validates the list of ingredients
    /// </summary>
    /// <param name="ingredients">List of ingredients</param>
    /// <exception cref="ArgumentException">If ingredients null or empty</exception>
    private static void CheckIngredients(List<Ingredient> ingredients) {
        if (ingredients == null) 
            throw new ArgumentException("Ingredients cannot be null");
        if (ingredients.Count == 0) 
            throw new ArgumentException("Ingredients cannot be empty");
    }

    /// <summary>
    /// Validates the list of tags
    /// </summary>
    /// <param name="tags">List of tags</param>
    /// <exception cref="ArgumentException">If null or too many tags</exception>
    private static void CheckTags(List<Tag> tags) {
        if (tags == null) 
            throw new ArgumentException("Tags cannot be null");
        if (tags.Count > Constants.MAX_TAGS) 
            throw new ArgumentException("Recipe can have a maximum of 3 tags");
    }

    /// <summary>
    /// Validates the list of steps
    /// </summary>
    /// <param name="steps">List of steps</param>
    /// <exception cref="ArgumentException">If steps null or empty</exception>
    private static void CheckSteps(List<Step> steps) {
        if (steps == null) 
            throw new ArgumentException("Steps cannot be null");
        if (steps.Count == 0) 
            throw new ArgumentException("Steps cannot be empty"); 
    }
    
    /// <summary>
    /// Makes a deep copy for Ingredients
    /// </summary>
    /// <param name="ingredients">Reference to constructor param</param>
    private void PopulateIngredients(List<Ingredient> ingredients) {
        Ingredients = new();
        foreach (Ingredient ingredient in ingredients) {
            Ingredients.Add(ingredient);
        }
    }

    /// <summary>
    /// Makes a deep copy for Steps
    /// </summary>
    /// <param name="steps">Reference to constructor param</param>
    private void PopulateSteps(List<Step> steps) {
        Steps = new();
        foreach (Step step in steps) {
            Steps.Add(step);
        }
    }

    /// <summary>
    /// Makes a deep copy for ratings
    /// </summary>
    /// <param name="ratings">Reference to constructor param</param>
    private void PopulateRatings(List<Rating> ratings) {
        Ratings = new();
        foreach (Rating rating in ratings) {
            Ratings.Add(rating);
        }
    }

    /// <summary>
    /// Makes a deep copy for tags
    /// </summary>
    /// <param name="tags">Reference to constructor param</param>
    private void PopulateTags(List<Tag> tags) {
        Tags = new();
        foreach (Tag tag in tags) {
            Tags.Add(tag);
        }
    }

    /// <summary>
    /// Gets the total time to cook for a recipe 
    /// </summary>
    /// <returns>Total time to complete all steps</returns>
    public int GetTimeToCook() {
        int timeToCook = 0;
        foreach (Step step in this.Steps) {
            timeToCook += step.TimeInMinutes;
        }
        return timeToCook;
    }
}
