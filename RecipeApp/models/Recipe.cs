namespace RecipeApp.Models;

using System.Text;

/// <summary>
/// Recipe schema
/// </summary>
public class Recipe {
    private string _name = null!;
    private string _description = null!;
    private int _servings;
    private List<Ingredient> _ingredients = null!;
    private List<Step> _steps = null!;
    private List<Rating> _ratings = null!;
    private List<Tag> _tags = null!;
    private User _user = null!;

    public int RecipeId { 
        get; 
        set; 
    }

    public double AverageRating{
        get => GetTotalAverageRating();
    }

    public string Name { 
        get => _name;  
        set {
            if (value == null) 
                throw new ArgumentException("Name cannot be null");
            if (value.Length == 0) 
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        } 
    }

    public string Description { 
        get => _description; 
        set {
            if (value == null) 
                throw new ArgumentException("Recipe description cannot be null");
            if (value.Length > Constants.MAX_DESCRIPTION_LENGTH)
                throw new ArgumentException($"Recipe description cannot exceed {Constants.MAX_DESCRIPTION_LENGTH} characters");
            _description = value;
        } 
    }

    public int Servings { 
        get => _servings; 
        set {
            if (value < Constants.MIN_SERVINGS) 
                throw new ArgumentException($"Serving(s) must be greater than {Constants.MIN_SERVINGS}");
            _servings = value;
        }
    }

    public List<Ingredient> Ingredients { 
        get => _ingredients; 
        set {
            if (value == null) 
                throw new ArgumentException("Ingredients cannot be null");
            if (value.Count == 0) 
                throw new ArgumentException("Ingredients cannot be empty");
            _ingredients = new();
            foreach (var ingredient in value) 
                _ingredients.Add(ingredient);
        } 
    }

    public List<Step> Steps { 
        get => _steps; 
        set {
            if (value == null) 
                throw new ArgumentException("Steps cannot be null");
            if (value.Count == 0) 
                throw new ArgumentException("Steps cannot be empty"); 
            _steps = new();
            foreach (var step in value) 
                _steps.Add(step);
        } 
    }

    public List<Rating> Ratings { 
        get => _ratings; 
        set {
            if (value == null) 
                throw new ArgumentException("Ratings cannot be null");
            _ratings = new();
            foreach (var rating in value) 
                _ratings.Add(rating);
        }
    }

    public List<Tag> Tags { 
        get => _tags; 
        set {
            if (value == null) 
                throw new ArgumentException("Tags cannot be null");
            if (value.Count > Constants.MAX_TAGS) 
                throw new ArgumentException("Recipe can have a maximum of 3 tags");
             _tags = new();
            foreach (var tag in value) 
                _tags.Add(tag);
        }
    }

    public User User { 
        get => _user;
        set {
            _user = value ?? throw new ArgumentException("User cannot be null");
        }
    }

    public string GetUserName{
        get => _user.Name;
    }

    public int UserId { get; set; }

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
    public Recipe(string name, User user, string description, int servings, List<Ingredient> ingredients, List<Step> steps, List<Rating> ratings, List<Tag> tags) {
        Name = name;
        User = user;
        Description = description;
        Servings = servings;
        Ingredients= ingredients;
        Steps = steps;
        Ratings = ratings;
        Tags = tags;
    }

    /// <summary>
    /// Empty constructor for entity framework
    /// </summary>
    public Recipe() {

    }

    /// <summary>
    /// Gets the total time to cook for a recipe 
    /// </summary>
    /// <returns>Total time to complete all steps</returns>
    public int GetTimeToCook() {
        int timeToCook = 0;
        foreach (Step step in _steps) 
            timeToCook += step.TimeInMinutes;
        return timeToCook;
    }

    /// <summary>
    /// Gets the total price of a recipe
    /// </summary>
    /// <returns>Price of the recipe</returns>
    public double GetTotalPrice() {
        double price = 0;
        foreach (Ingredient ingredient in _ingredients) 
            price += ingredient.Price;
        return price;
    }

    /// <summary>
    /// This method gets the avarage of ratings of a recipe.
    /// </summary>
    /// <returns>The ratings.</returns>
    public double GetTotalAverageRating(){
        double rating = 0;
        foreach(Rating r in _ratings )
            rating += r.Stars;
        return rating/_ratings.Count;
    }

    

    /// <summary>
    /// Returns string representation of a recipe
    /// </summary>
    /// <returns>String format of recipe</returns>
    public override string ToString() {
        StringBuilder builder = new();
        builder.Append("Name: " + _name + "\n");
        builder.Append("Description: " + _description + "\n");
        builder.Append("Servings: " + _servings + "\n"); 
        builder.Append("Ingredients: \n");
        builder.Append("-------------- \n");
        foreach (Ingredient ingredient in _ingredients) {
            builder.Append(ingredient.ToString() + "\n");
        }
        builder.Append("-------------- \n");
        builder.Append("Steps: \n");
        builder.Append("-------------- \n");
        foreach (Step step in _steps) {
            builder.Append(step.ToString() + "\n");
        }
        builder.Append("-------------- \n");
        builder.Append("Tags: \n");
        builder.Append("-------------- \n");
        foreach (Tag tag in _tags) {
            builder.Append(tag.ToString() + "\n");
        }
        builder.Append("-------------- \n");
        builder.Append("Reviews: \n");
        builder.Append("-------------- \n");
        foreach (Rating rating in _ratings) {
            builder.Append(rating.ToString() + "\n");
        }
        builder.Append("--------------");
        return builder.ToString();
    }
}
