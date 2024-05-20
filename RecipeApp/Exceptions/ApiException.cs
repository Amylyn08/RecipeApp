namespace RecipeApp.Exceptions;

/// <summary>
/// Custom exception for when our API fails
/// </summary>
public class ApiException : Exception {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message">Error message specific to API</param>
    public ApiException(string message) : base(message) {}
}