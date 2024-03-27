namespace RecipeApp.Services;

using RecipeApp.Models;

public class UserService {
    public User Login(string username, string password) {
        if (username == null || password == null)
            throw new ArgumentException("Username and passwod cannot be null");
        foreach (User user in MockDatabase.Users) {
            if (user.Name.Equals(username) && user.Password.Equals(password)) {
                return user;
            }
        }
        return null;
    }

    public User Register(string username, string password, string description) {
        if (username == null || password == null || description == null)
            throw new ArgumentException("Username/password/description cannot be null");
        User user = new(username, description, password, new(), new());
        MockDatabase.Users.Add(user);
        return user;
    }
}