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
        foreach (User mock in MockDatabase.Users) {
            if (mock.Name.Equals(username)) {
                return null;
            }
        }
        MockDatabase.Users.Add(user);
        return user;
    }

    public void ChangePassword(User user, string newPassword) {
        if (user is null) 
            throw new ArgumentException("User cannot be null");
        if (newPassword.Length < Constants.MIN_PASS_LENGTH)
            throw new ArgumentException($"Password must be atleast {Constants.MIN_PASS_LENGTH} characters");
        foreach (User mock in MockDatabase.Users) {
            if (mock == user) {
                mock.Password = newPassword;
                break;
            }
        }
    }

    public void DeleteAccount(User user) {
        if (user is null) 
            throw new ArgumentException("User cannot be null");
        MockDatabase.Users.Remove(user);
    }

    public void AddToFavourites(Recipe favourited, User user) {
        if (favourited == null || user == null) 
            throw new ArgumentException("Recipe or user cannot be null");
        foreach (User mock in MockDatabase.Users) {
            if (mock.Equals(user)) {
                if (mock.Favorites.Contains(favourited)) {
                    throw new ArgumentException("This recipe has already been favourited");
                }
                mock.Favorites.Add(favourited);
                break;
            }
        }
    }
}       