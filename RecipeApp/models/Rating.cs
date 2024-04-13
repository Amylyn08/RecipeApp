namespace RecipeApp.Models;

/// <summary>
/// For rating of recipes, contains stars, description, and user/author of the rating.
/// </summary>
public class Rating {
    private int _stars;
    private string _description;
    private User _user;

    public Recipe Recipe {
        get; 
        set;
    }

    public int RatingId { 
        get; 
        set; 
    }

    public int Stars {
        get => _stars; 
        set {
            if (value > 5 || value < 0) 
                throw new ArgumentException("Stars must be between 0 to 5");
            _stars = value;
        }
    }

    public string Description{
        get => _description; 
        set {
            if (value.Length > Constants.MAX_DESCRIPTION_LENGTH) 
                throw new ArgumentException("Max description length exceeded!");
            if (value is null) 
                _description = "";
            else 
                _description = value;
        }
    }

    public User User {
        get => _user; 
        set {
            if (value == null) 
                throw new ArgumentException("User cannot be null");
            _user = value;
        }
    }

    /// <summary>
    /// Constructor of Rating.
    /// </summary>
    /// <param name="stars">Stars for rating out of 5</param>
    /// <param name="desc">Dscription of the rating</param>
    /// <param name="user">User who created the rating</param>
    /// <exception cref="ArgumentException">Throws exceptions if user disrespects contraints</exception>
    public Rating(int stars, string description, User user){
        Stars = stars;
        Description = description;
        User = user;
    }

    /// <summary>
    /// Empty constructor for Entity framework
    /// </summary>
    public Rating() {

    }

    /// <summary>
    /// Overriden ToString()
    /// </summary>
    /// <returns>String representation of a rating</returns>
    public override string ToString() {
        return "User: " + User.Name + "\n" + "Stars: " + Stars + "\n" + "Description: " + Description;
    }
}