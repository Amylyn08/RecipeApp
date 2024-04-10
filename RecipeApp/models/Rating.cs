namespace RecipeApp.Models;

/// <summary>
/// For rating of recipes, contains stars, description, and user/author of the rating.
/// </summary>
public class Rating {
    private int _stars;
    private string _description;

    public int Stars{get => _stars; set {
        if (value > 5 || value < 0) throw new ArgumentException("Stars must be between 0 to 5");
        _stars = value;
    }}
    public string Description{get => _description; set {
        if(value.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Max description length exceeded!");
        _description = value;
    }}
    public User User{get; private set;}

    /// <summary>
    /// Constructor of Rating.
    /// </summary>
    /// <param name="stars">Stars for rating out of 5</param>
    /// <param name="desc">Dscription of the rating</param>
    /// <param name="user">User who created the rating</param>
    /// <exception cref="ArgumentException">Throws exceptions if user disrespects contraints</exception>
    public Rating(int stars, string desc, User user){
        if (stars > 5 || stars < 0) throw new ArgumentException("Stars must be between 0 to 5");
        if(user == null) throw new ArgumentException("User cannot be null");
        desc ??= "";
        if(desc.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Max description length exceeded!");

        Stars = stars;
        Description = desc;
        User = user;
    }

    public override string ToString()
    {
        return "User: " + User.Name + "\n" + "Stars: " + Stars + "\n" + "Description: " + Description;
    }
}