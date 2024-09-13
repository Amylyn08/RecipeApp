namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown during invalid authentication
/// </summary>
public class InvalidCredentialsException : Exception {
    /// <summary>
    /// Constructs a new InvalidCredentialsException
    /// </summary>
    /// <param name="message">Some error of our choosing</param>
    public InvalidCredentialsException(string message) : base (message) {}
}