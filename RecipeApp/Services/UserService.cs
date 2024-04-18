namespace RecipeApp.Services;

using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Security;

public class UserService : ServiceBase {
    public IEncrypter Encrypter { get; set; }

    public UserService(IEncrypter encrypter) {
        Encrypter = encrypter;
    }

    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="username">Username from client</param>
    /// <param name="password">Raw password from client</param>
    /// <returns>User object from database</returns>
    /// <exception cref="UserDoesNotExistException">If the username provided does not correspond with an account</exception>
    /// <exception cref="InvalidCredentialsException">If the password is not valid for the given username</exception>
    public User Login(string username, string password) {
        var userInDatabase = Context.Users.Where(u => u.Name.Equals(username)).First();
        if (userInDatabase is null) {
            throw new UserDoesNotExistException($"User ${username} does not exist !");
        }
        var encryptedPasswordFromDatabase = userInDatabase.Password;
        var encryptedPassword = Encrypter.Encrypt(password); 
        if (!encryptedPasswordFromDatabase.Equals(encryptedPassword)) {
            throw new InvalidCredentialsException("Invalid credentials provided !");
        }
        return userInDatabase;
    }

    /// <summary>
    /// Adds a user to the database
    /// </summary>
    /// <param name="userToAdd">User object to add</param>
    /// <exception cref="UserAlreadyExistsException">If user with same username already exists</exception>
    /// <exception cref="ArgumentException">If userToAdd is null</exception>
    public void Register(User userToAdd) {
        if (userToAdd is null) {
            throw new ArgumentException("User to add cannot be null");
        }
        var userInDatabase = Context.Users.Where(u => u.Name.Equals(userToAdd.Name)).First();
        if (userInDatabase is not null) {
            throw new UserAlreadyExistsException($"User {userToAdd.Name} already exists !");
        }
        userToAdd.Password = Encrypter.Encrypt(userToAdd.Password);
        Context.Add(userToAdd);
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

    public void DeleteFromFavourites(Recipe toDelete, User user) {
        if (toDelete == null || user == null) 
            throw new ArgumentException("Recipe or user cannot be null");
        foreach (User mock in MockDatabase.Users) {
            if (mock.Equals(user)) {
                mock.Favorites.Remove(toDelete);
            }
        }
    }
}       