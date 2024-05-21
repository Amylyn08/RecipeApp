namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown when a user attempts to favourite a recipe that they 
/// have already favourited
/// </summary>
public class AlreadyFavouritedException : Exception {
    /// <summary>
    /// Empty constructor
    /// </summary>
    public AlreadyFavouritedException() : base("You cannot favourite the same recipe twice !")
    {}
}