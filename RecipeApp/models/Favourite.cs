namespace RecipeApp.Models;

/// <summary>
/// Represents bridging table between users and recipes
/// </summary>
public class Favourite {
    public int FavouriteId { get; set; }

    private Recipe? _recipe;
    private User? _user;

    /// <summary>
    /// Get setters for Recipe. Checks if null first before assignings.
    /// </summary>
    public Recipe? Recipe { 
        get => _recipe; 
        set{
            if(value is null){
                throw new ArgumentException("The recipe cannot be null");
            }
            _recipe = value;
        } 
    }

    public int RecipeId { get; set; }

    /// <summary>
    /// Getter and setter for User. Checks if user is null before assigning.
    /// </summary>
    public User? User { 
        get => _user; 
        set{
            if(value is null){
                throw new ArgumentException("User cannot be null");
            }
            _user = value;
        } 
    }

    public int UserId { get; set; }

    /// <summary>
    /// Empty constructor for entity framework.
    /// </summary>
    public Favourite(){

    }
    /// <summary>
    /// Constructor for favorite.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <param name="user">The user object.</param>
    public Favourite(Recipe recipe, User user){
        Recipe = recipe;
        User = user;
    }

    /// <summary>
    /// Overriding equals method for favorites.
    /// </summary>
    /// <param name="obj">The other potential favourite object.</param>
    /// <returns>Whether or not this object and obj are the same.</returns>
    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if(obj.GetType() != typeof(Favourite)) return false;
        Favourite other = (Favourite) obj;
        if (_recipe is not null && _user is not null)
            return _recipe.Equals(other.Recipe) && _user.Equals(other.User);
        return false;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}