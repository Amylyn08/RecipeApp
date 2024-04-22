namespace RecipeApp.Services;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Security;

/// <summary>
/// Performs user related business logiv
/// </summary>
public class UserService : ServiceBase {
    private PasswordEncrypter _encrypter = null!;
    private SplankContext _context = null!;

    public PasswordEncrypter Encrypter { 
        get => _encrypter; 
        set {
            if (value is null) {
                throw new ArgumentException("Encrypter cannot be null");
            }
            _encrypter = value;
        }  
    }

    /// <summary>
    /// Constructor with encrypter
    /// </summary>
    /// <param name="encrypter">Encrypter for passwords</param>
    public UserService(SplankContext context, PasswordEncrypter encrypter) : base(context) {
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
        if (username is null) {
            throw new ArgumentException("Username cannot be null");
        }
        if (password is null) {
            throw new ArgumentException("Password cannot be null");
        }
        User? userInDatabase = Context.Users.Where(u => u.Name.Equals(username)).FirstOrDefault();
        if (userInDatabase is null) {
            throw new UserDoesNotExistException($"User ${username} does not exist !");
        }
        var encryptedPasswordFromDatabase = userInDatabase.Password;
        var saltFromDatabase = userInDatabase.Salt;
        var encryptedPassword = Encrypter.CreateHash(password, userInDatabase.Salt); 
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
    public void Register(string username, string password, string description) {
        if (string.IsNullOrWhiteSpace(username)) {
            throw new ArgumentException("Username cannot be null");
        }
        if (string.IsNullOrWhiteSpace(password)) {
            throw new ArgumentException("Password cannot be null");
        }
        var userInDatabase = Context.Users.Where(u => u.Name.Equals(username)).FirstOrDefault();
        if (userInDatabase is not null) {
            throw new UserAlreadyExistsException($"User {username} already exists !");
        }
        var salt = Encrypter.CreateSalt();
        var hashedPassword = Encrypter.CreateHash(password, salt);
        var userToAdd = new User(username, description, hashedPassword, new(), new(), salt);
        Context.Add(userToAdd);
        Context.SaveChanges();
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
        var salt = Encrypter.CreateSalt();
        var hashedPassword = Encrypter.CreateHash(userToChangePassword.Password, salt);
        userToChangePassword.Password = hashedPassword;
        userToChangePassword.Salt = salt;
        Context.Update(userToChangePassword);
        Context.SaveChanges();
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="userToDelete">User to delete</param>
    /// <exception cref="ArgumentException">If user to delete is null</exception>
    public void DeleteAccount(User userToDelete) {
        if (userToDelete is null) {
            throw new ArgumentException("User to delete cannot be null");
        }
        Context.Remove(userToDelete);
        Context.SaveChanges();
    }

    /// <summary>
    /// Adds a entry into the favourites bridging table
    /// </summary>
    /// <param name="favourited">Recipe that was favourited</param>
    /// <param name="user">User who favourited that recipe</param>
    /// <exception cref="ArgumentException">If recipe or user is null</exception>
    public void AddToFavourites(Recipe favourited, User user) {
        if (favourited is null) {
            throw new ArgumentException("Favourited recipe cannot be null !");
        }
        if (user is null) {
            throw new ArgumentException("User cannot be null !");
        }
        var alreadyFavourited = Context.Favourites
            .Where(f => f.Recipe.Equals(favourited) && f.User.Equals(user)).FirstOrDefault();
        if (alreadyFavourited is not null) {
            throw new AlreadyFavouritedException();
        }
        Favourite favouriteEntry = new() {
            Recipe = favourited,
            User = user
        };
        Context.Add(favouriteEntry);
        Context.SaveChanges();
    }

    /// <summary>
    /// Deletes a favourite entry
    /// </summary>
    /// <param name="favouriteToDelete">Entry to delete</param>
    /// <exception cref="ArgumentException">If entry is null</exception>
    public void DeleteFromFavourites(Favourite favouriteToDelete) {
        if (favouriteToDelete is null) {
            throw new ArgumentException("Favourite to delete cannot be null !");
        }
        Context.Remove(favouriteToDelete);
        Context.SaveChanges();
    }
}       