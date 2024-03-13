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

    public User(string name, string desc, string pass, List<Recipe> favorites) {
        if(name == null) throw new ArgumentException("Name cannot be null!");
        if(name.Length == 0) throw new ArgumentException("Name cannot be empty!");

        this.Name = name;
        this.Description = desc;
        this.Password = pass;
        this.Favorites = favorites;
    }
}