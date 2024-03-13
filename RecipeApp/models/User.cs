namespace RecipeApp.Models;
/// <summary>
/// Represents a user
/// </summary>
public class User {
    public string Name { get; private set;}
    //Profile pic --> will implement when teacher shows us
    public string Description {get; private set;}
    public string Password;
    public List<Recipe> Favorites{get; private set;}

    /// <summary>
    /// Constructor to create a User
    /// </summary>
    /// <param name="name">Username of the user</param>
    /// <param name="description">Description of the user, can be empty</param>
    /// <param name="pass">Password of the user</param>
    /// <param name="favorites">List of recipes that the user favorited</param>
    /// <exception cref="ArgumentException">If any field is null or does not respect the specific constraints, it throws an exception</exception>
    public User(string name, string description, string pass, List<Recipe> favorites) {
        if(name == null) throw new ArgumentException("Name cannot be null!");
        if(name.Length < MIN_NAME_LENGTH) throw new ArgumentException("Name cannot be less than 2 characters!");
        if(name.Length > MAX_NAME_LENGTH) throw new ArgumentException("Name cannot be more than 15 characters!");
        if(pass == null) throw new ArgumentException("Password cannot be null!");
        if(pass.Length < MIN_PASS_LENGTH) throw new ArgumentException("Password needs to be atleast 8 characters!");
        if(description == null) {
            Description = "";
        }
        Name = name;
        Description = description;
        Password = pass;
        Favorites = favorites;
    }

    const int MAX_NAME_LENGTH = 15;
    const int MIN_NAME_LENGTH = 2;
    const int MIN_PASS_LENGTH = 8;
}