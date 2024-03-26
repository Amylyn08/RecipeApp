namespace RecipeApp.Models;

using System.Text;

/// <summary>
/// Recipe schema
/// </summary>
public class Recipe {
    private string _name;
    private string _description;
    private int _servings;
    private List<Ingredient> _ingredients;
    private List<Step> _steps;
    private List<Rating> _ratings;
    private List<Tag> _tags;
    private readonly User _user;

    public string Name { 
        get => _name;  
        set {
            CheckName(value);
            _name = value;
        } 
    }

    public string Description { 
        get => _description; 
        set {
            CheckDescription(value);
            _description = value;
        } 
    }

    public int Servings { 
        get => _servings; 
        set {
            CheckServings(value);
            _servings = value;
        }
    }

    public List<Ingredient> Ingredients { 
        get => _ingredients; 
        set {
            CheckIngredients(value);
            PopulateIngredients(value);
        } 
    }

    public List<Step> Steps { 
        get => _steps; 
        set {
            CheckSteps(value);
            PopulateSteps(value);
        } 
    }

    public List<Rating> Ratings { 
        get => _ratings; 
        set {
            CheckRatings(value);
            PopulateRatings(value);
        }
    }

    public List<Tag> Tags { 
        get => _tags; 
        set {
            CheckTags(value);
            PopulateTags(value);
        }
    }

    public User User { 
        get => _user; 
    }

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
        CheckName(name);
        CheckUser(user);
        CheckDescription(description);
        CheckServings(servings);
        CheckIngredients(ingredients);
        CheckSteps(steps);
        CheckRatings(ratings);
        CheckTags(tags);
        _name = name;
        _user = new(user.Name, user.Description, user.Password, user.Favorites, user.MadeRecipes);
        _description = description;
        _servings = servings;
        PopulateIngredients(ingredients);
        PopulateSteps(steps);
        PopulateRatings(ratings);
        PopulateTags(tags);
    }

    /// <summary>
    /// Validates the user field
    /// </summary>
    /// <param name="user">User who owns the recipe</param>
    /// <exception cref="ArgumentException">If user is null</exception>
    private static void CheckUser(User user) {
        if (user == null) 
            throw new ArgumentException("User cannot be null");
    }

    /// <summary>
    /// Validates the name field
    /// </summary>
    /// <param name="name">Name of recipe</param>
    /// <exception cref="ArgumentException">If null or empty</exception>
    private static void CheckName(string name) {
        if (name == null) 
            throw new ArgumentException("Name cannot be null");
        if (name.Length == 0) 
            throw new ArgumentException("Name cannot be empty");
    }

    /// <summary>
    /// Validate the description field
    /// </summary>
    /// <param name="description">Description of recipe</param>
    /// <exception cref="ArgumentException">If description null, or too long</exception>
    private static void CheckDescription(string description) {
        if (description == null) 
            throw new ArgumentException("Recipe description cannot be null");
        if (description.Length > Constants.MAX_DESCRIPTION_LENGTH)
            throw new ArgumentException($"Recipe description cannot exceed {Constants.MAX_DESCRIPTION_LENGTH} characters");
    }

    /// <summary>
    /// Validates the serving field
    /// </summary>
    /// <param name="servings">Servings for recipe</param>
    /// <exception cref="ArgumentException">If servings does not meet minimum amount</exception>
    private static void CheckServings(int servings) {
        if (servings < Constants.MIN_SERVINGS) 
            throw new ArgumentException($"Serving(s) must be greater than {Constants.MIN_SERVINGS}");
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
    /// Validates the ratings field
    /// </summary>
    /// <param name="ratings">Ratings of the recipe</param>
    /// <exception cref="ArgumentException">If ratings is null</exception>
    private static void CheckRatings(List<Rating> ratings) {
        if (ratings == null) 
            throw new ArgumentException("Ratings cannot be null");
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
    /// <param name="ingredients">Reference to constructor param or setter value</param>
    private void PopulateIngredients(List<Ingredient> ingredients) {
        _ingredients = new();
        foreach (Ingredient ingredient in ingredients) {
            _ingredients.Add(ingredient);
        }
    }

    /// <summary>
    /// Makes a deep copy for Steps
    /// </summary>
    /// <param name="steps">Reference to constructor param or setter value</param>
    private void PopulateSteps(List<Step> steps) {
        _steps = new();
        foreach (Step step in steps) {
            _steps.Add(step);
        }
    }

    /// <summary>
    /// Makes a deep copy for ratings
    /// </summary>
    /// <param name="ratings">Reference to constructor param or setter value</param>
    private void PopulateRatings(List<Rating> ratings) {
        _ratings = new();
        foreach (Rating rating in ratings) {
            _ratings.Add(rating);
        }
    }

    /// <summary>
    /// Makes a deep copy for tags
    /// </summary>
    /// <param name="tags">Reference to constructor param or setter value</param>
    private void PopulateTags(List<Tag> tags) {
        _tags = new();
        foreach (Tag tag in tags) {
            _tags.Add(tag);
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

    public double GetTotalPrice() {
        double price = 0;
        foreach (Ingredient ingredient in Ingredients) {
            price += ingredient.Price;
        }
        return price;
    }

    public override string ToString() {
        StringBuilder builder = new();
        builder.Append("Username: " + User.Name + "\n");
        builder.Append("Description: " + Description + "\n");
        builder.Append("Ingredients: \n");
        builder.Append("Servinsg: " + Servings + "\n"); 
        foreach (Ingredient ingredient in Ingredients) {
            builder.Append(ingredient.ToString() + "\n");
        }
        builder.Append("Steps: \n");
        foreach (Step step in Steps) {
            builder.Append(step.ToString() + "\n");
        }
        builder.Append("Tags: \n");
        foreach (Tag tag in Tags) {
            builder.Append(tag.ToString() + "\n");
        }
        builder.Append("Reviews: \n");
        foreach (Rating rating in Ratings) {
            builder.Append(rating.ToString() + "\n");
        }
        return builder.ToString();
    }
}
