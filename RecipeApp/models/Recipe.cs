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
    public List<string> Steps { get; private set; }
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
    public Recipe(  User user, 
                    string description, 
                    int servings, 
                    List<Ingredient> ingredients, 
                    List<string> steps, 
                    List<Rating> ratings, 
                    List<Tag> tags) {

        const int MAX_TAGS = 3; 
        const int MIN_SERVINGS = 1;

        if (user == null) throw new ArgumentException("User cannot be null");
        if (description == null) description = "";
        if (description.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Description exceeds maximum of " + Constants.MAX_DESCRIPTION_LENGTH);
        if (servings < MIN_SERVINGS) throw new ArgumentException("Serving(s) must be greater than 0");
        if (ingredients == null) throw new ArgumentException("Ingredients cannot be null");
        if (steps == null) throw new ArgumentException("Steps cannot be null");
        if (ratings == null) throw new ArgumentException("Ratings cannot be null");
        if (tags == null) throw new ArgumentException("Tags cannot be null");
        if (tags.Count > MAX_TAGS) throw new ArgumentException("Recipe can have a maximum of 3 tags"); 
        if (ingredients.Count == 0) throw new ArgumentException("Ingredients cannot be empty");
        if (steps.Count == 0) throw new ArgumentException("Steps cannot be empty");

        this.User = user;
        this.Description = description;
        this.Servings = servings;
        this.Ingredients = new List<Ingredient>();
        this.Steps = new List<string>();
        this.Ratings = new List<Rating>();
        this.Tags = new List<Tag>();

        this.PopulateIngredients(ingredients);
        this.PopulateSteps(steps);
        this.PopulateRatings(ratings);
        this.PopulateTags(tags);
    }
    
    /// <summary>
    /// Makes a deep copy for Ingredients
    /// </summary>
    /// <param name="ingredients">Reference to constructor param</param>
    private void PopulateIngredients(List<Ingredient> ingredients) {
        foreach (Ingredient ingredient in ingredients) {
            this.Ingredients.Add(ingredient);
        }
    }

    /// <summary>
    /// Makes a deep copy for Steps
    /// </summary>
    /// <param name="steps">Reference to constructor param</param>
    private void PopulateSteps(List<string> steps) {
        foreach (string step in steps) {
            this.Steps.Add(step);
        }
    }

    /// <summary>
    /// Makes a deep copy for ratings
    /// </summary>
    /// <param name="ratings">Reference to constructor param</param>
    private void PopulateRatings(List<Rating> ratings) {
        foreach (Rating rating in ratings) {
            this.Ratings.Add(rating);
        }
    }

    /// <summary>
    /// Makes a deep copy for tags
    /// </summary>
    /// <param name="tags">Reference to constructor param</param>
    private void PopulateTags(List<Tag> tags) {
        foreach (Tag tag in tags) {
            this.Tags.Add(tag);
        }
    }


}