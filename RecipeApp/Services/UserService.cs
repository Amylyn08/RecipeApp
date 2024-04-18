namespace RecipeApp.Services;

using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Security;

/// <summary>
/// Performs user related business logiv
/// </summary>
public class UserService : ServiceBase {
    private IEncrypter _encrypter;

    public IEncrypter Encrypter { 
        get => _encrypter; 
        set {
            if (value == null) {
                throw new ArgumentException("Encrypter cannot be null");
            }
            _encrypter = value;
        }  
    }

    /// <summary>
    /// Constructor with encrypter
    /// </summary>
    /// <param name="encrypter">Encrypter for passwords</param>
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
        var userInDatabase = Context.Users.Where(u => u.Name.Equals(userToAdd.Name)).FirstOrDefault();
        if (userInDatabase is not null) {
            throw new UserAlreadyExistsException($"User {userToAdd.Name} already exists !");
        }
        userToAdd.Password = Encrypter.Encrypt(userToAdd.Password);
        Context.Add(userToAdd);
    }

    /// <summary>
    /// Changes a users password
    /// </summary>
    /// <param name="userToChangePassword">User object to update</param>
    /// <param name="newPassword">New password of user</param>
    /// <exception cref="ArgumentException">If user is null or new password is null/empty</exception>
    public void ChangePassword(User userToChangePassword, string newPassword) {
        if (userToChangePassword is null) {
            throw new ArgumentException("User cannot be null !");
        }
        if (newPassword is null) {
            throw new ArgumentException("New password cannot be null !");
        }
        if (newPassword.Length < Constants.MIN_PASS_LENGTH) {
            throw new ArgumentException($"New password must be at least {Constants.MIN_PASS_LENGTH} characters long !");
        }
        var encryptedPassword = Encrypter.Encrypt(newPassword);
        userToChangePassword.Password = encryptedPassword;
        Context.Update(userToChangePassword);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="userToDelete">User to delete</param>
    public void DeleteAccount(User userToDelete) {
        Context.Remove(userToDelete);
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