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
    /// <param name="desc">Description of the user, can be empty</param>
    /// <param name="pass">Password of the user</param>
    /// <param name="favorites">List of recipes that the user favorited</param>
    /// <exception cref="ArgumentException"></exception>
    public User(string name, string desc, string pass, List<Recipe> favorites) {
        if(name == null) throw new ArgumentException("Name cannot be null!");
        if(name.Length == 0) throw new ArgumentException("Name cannot be empty!");
        if(pass == null) throw new ArgumentException("Password cannot be null!");
        if(pass.Length == 0) throw new ArgumentException("Password cannot be empty!");
        if(desc == null) {
            Description = "";
        }

        Name = name;
        Description = desc;
        Password = pass;
        Favorites = favorites;
    }
}