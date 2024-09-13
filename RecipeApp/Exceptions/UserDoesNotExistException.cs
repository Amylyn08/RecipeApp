namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown when a user who logs in with a username 
/// that doesnt exist
/// </summary>
public class UserDoesNotExistException : Exception {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Error message</param>
    public UserDoesNotExistException(string message) : base (message) {}
}