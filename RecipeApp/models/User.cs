

using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Models;

/// <summary>
/// Represents a user
/// </summary>
public class User {
    private string _password = null!;
    private string _name = null!;
    private string _description = null!;
    private string _salt = null!;
    private List<Recipe> _favourites = null!;
    private List<Recipe> _madeRecipes; 
    private byte[] _profilePicture;

    public int UserId {
        get; 
        set;
    }

    public string Salt {
        get => _salt;
        set {
            if (value is null) {
                throw new ArgumentException("Salt cannot be null");
            }
            _salt = value;
        }
    }

    public string Name { 
        get => _name; 
        set {
            if (value is null) 
                throw new ArgumentException("Name cannot be null!");
            if (value.Length < Constants.MIN_NAME_LENGTH) 
                throw new ArgumentException("Name cannot be less than 2 characters!");
            if (value.Length > Constants.MAX_NAME_LENGTH) 
                throw new ArgumentException("Name cannot be more than 15 characters!");
            _name = value;
    }}

    public string Description {
        get => _description; 
        set {
            value ??= "";
            if (value.Length > Constants.MAX_DESCRIPTION_LENGTH) 
                throw new ArgumentException("Description passed the limit character of " + Constants.MAX_DESCRIPTION_LENGTH);
            _description = value;
        }
    }

    public string Password {
        get => _password; 
        set {
            if (value is null) 
                throw new ArgumentException("Password cannot be null!");
            if (value.Length < Constants.MIN_PASS_LENGTH) 
                throw new ArgumentException("Password needs to be atleast 8 characters!");
            _password = value;
        }
    }

    [NotMapped]
    public List<Recipe> Favorites {
        get => _favourites; 
        set {
            if (value is null) 
                throw new ArgumentException("Favourites cannot be null");
            _favourites = new();
            foreach (var recipe in value)
                _favourites.Add(recipe);
        }
    }

    public List<Recipe> MadeRecipes {
        get => _madeRecipes;
        set {
            if (value is null)
                throw new ArgumentException("Made Recipes cannot be null !");
            _madeRecipes = new();
            foreach (var recipe in value)
                _madeRecipes.Add(recipe);
        }
    }

    public byte[] ProfilePicture {
        get => _profilePicture;
        set {
            if (value is null) 
                throw new ArgumentException("Profile picture cannot be null");
            _profilePicture = value;
        }
    }

    /// <summary>
    /// Constructor to create a User
    /// </summary>
    /// <param name="name">Username of the user</param>
    /// <param name="description">Description of the user, can be empty</param>
    /// <param name="pass">Password of the user</param>
    /// <param name="favorites">List of recipes that the user favorited</param>
    /// <param name="madeRecipes">List of recipes that the user made</param>
    /// <exception cref="ArgumentException">If any field is null or does not respect the specific constraints, it throws an exception</exception>
    public User(string name, string description, string pass, List<Recipe> favorites, List<Recipe> madeRecipes, string salt) {
        Name = name;
        Description = description;
        Password = pass;
        Favorites = favorites;
        MadeRecipes = madeRecipes;
        Salt = salt;
    }

    /// <summary>
    /// Empty constructor for entity framework
    /// </summary>
    public User() {

    }

    /// <summary>
    /// Overriden Equals()
    /// </summary>
    /// <param name="obj">Object to compare against</param>
    /// <returns>If obj is a User and has the same name</returns>
    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (obj.GetType() != typeof(User)) return false;
        User other = (User) obj;
        return Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public override string ToString() {
        return Name;
    }
}