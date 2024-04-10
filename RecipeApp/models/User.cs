namespace RecipeApp.Models;

/// <summary>
/// Represents a user
/// </summary>
public class User {
    private string _password;
    private string _name;
    private string _descrition;

    public string Name { get => _name; set{
        if(value == null) throw new ArgumentException("Name cannot be null!");
        if(value.Length < Constants.MIN_NAME_LENGTH) throw new ArgumentException("Name cannot be less than 2 characters!");
        if(value.Length > Constants.MAX_NAME_LENGTH) throw new ArgumentException("Name cannot be more than 15 characters!");
        _name = value;
    }}
    //Profile pic --> will implement when teacher shows us
    public string Description {get => _descrition; set {
        value ??= "";
        if(value.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Description passed the limit character of " + Constants.MAX_DESCRIPTION_LENGTH);
        _descrition = value;
    }}
    public string Password{get => _password; set {
        if(value == null) throw new ArgumentException("Password cannot be null!");
        if(value.Length < Constants.MIN_PASS_LENGTH) throw new ArgumentException("Password needs to be atleast 8 characters!");
        _password = value;
    }}
    public List<Recipe> Favorites{get; private set;}
    public List<Recipe> MadeRecipes{get; private set;}
    /// <summary>
/// Constructor to create a User
    /// </summary>
    /// <param name="name">Username of the user</param>
    /// <param name="description">Description of the user, can be empty</param>
    /// <param name="pass">Password of the user</param>
    /// <param name="favorites">List of recipes that the user favorited</param>
    /// <param name="madeRecipes">List of recipes that the user made</param>
    /// <exception cref="ArgumentException">If any field is null or does not respect the specific constraints, it throws an exception</exception>
    public User(string name, string description, string pass, List<Recipe> favorites, List<Recipe> madeRecipes) {
        if(name == null) throw new ArgumentException("Name cannot be null!");
        if(pass == null) throw new ArgumentException("Password cannot be null!");
        description ??= "";
        if(name.Length < Constants.MIN_NAME_LENGTH) throw new ArgumentException("Name cannot be less than 2 characters!");
        if(name.Length > Constants.MAX_NAME_LENGTH) throw new ArgumentException("Name cannot be more than 15 characters!");
        if(pass.Length < Constants.MIN_PASS_LENGTH) throw new ArgumentException("Password needs to be atleast 8 characters!");
        if(description.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Description passed the limit character of " + Constants.MAX_DESCRIPTION_LENGTH);
        if(favorites == null) throw new ArgumentException("List can't be null");
        if(madeRecipes == null) throw new ArgumentException("List can't be null");
        Name = name;
        Description = description;
        Password = pass;
        Favorites = favorites;
        MadeRecipes = madeRecipes;
    }

    public override bool Equals(object? obj) {
        if (obj.GetType() != typeof(User)) return false;
        User other = (User) obj;
        return Name == other.Name;
    }
}