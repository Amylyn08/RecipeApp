namespace RecipeApp.Services;

using RecipeApp.Models;

public class UserService {
    private readonly MockDatabase db;

    public User Login(string username, string password) {
        if (username == null || password == null)
            throw new ArgumentException("Username and passwod cannot be null");
        foreach (User user in db.Users) {
            if (user.Name.Equals(username) && user.Name.Equals(password)) {
                return user;
            }
        }
        return null;
    }

    public User Register(string username, string password, string description) {
        if (username == null || password == null || description == null)
            throw new ArgumentException("Username/password/description cannot be null");
        User user = new(username, description, password, new(), new());
        db.Users.Add(user);
        return user;
    }
}