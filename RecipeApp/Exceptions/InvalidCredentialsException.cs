namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown during invalid authentication
/// </summary>
public class InvalidCredentialsException : Exception {
    public InvalidCredentialsException(string message) : base (message) {}
}