namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown during registration when a user chooses 
/// a username that is already reserved
/// </summary>
public class UserAlreadyExistsException : Exception {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Error Message</param>
    public UserAlreadyExistsException(string message) : base (message) {}
}