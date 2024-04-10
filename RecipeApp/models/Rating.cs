namespace RecipeApp.Models;

/// <summary>
/// For rating of recipes, contains stars, description, and user/author of the rating.
/// </summary>
public class Rating {
    private int _stars;
    private string _description;
    private User _user;

    public int RatingId { 
        get; 
        set; 
    }

    public int Stars{
        get => _stars; 
        set {
            if (value > 5 || value < 0) throw new ArgumentException("Stars must be between 0 to 5");
            _stars = value;
        }
    }
    public string Description{get => _description; set {
        if(value.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Max description length exceeded!");
        _description = value;
    }}
    public User User{get => _user; set => _user = value;}

    /// <summary>
    /// Constructor of Rating.
    /// </summary>
    /// <param name="stars">Stars for rating out of 5</param>
    /// <param name="desc">Dscription of the rating</param>
    /// <param name="user">User who created the rating</param>
    /// <exception cref="ArgumentException">Throws exceptions if user disrespects contraints</exception>
    public Rating(int stars, string description, User user){
        if (stars > 5 || stars < 0) throw new ArgumentException("Stars must be between 0 to 5");
        if(user == null) throw new ArgumentException("User cannot be null");
        description ??= "";
        if(description.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Max description length exceeded!");

        Stars = stars;
        Description = description;
        User = user;
    }

    /// <summary>
    /// Empty constructor for Entity framework
    /// </summary>
    public Rating() {}

    public override string ToString()
    {
        return "User: " + User.Name + "\n" + "Stars: " + Stars + "\n" + "Description: " + Description;
    }
}