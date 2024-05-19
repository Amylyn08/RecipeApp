namespace RecipeApp.Exceptions;

/// <summary>
/// Thrown when a user attempts to favourite a recipe that they 
/// have already favourited
/// </summary>
public class AlreadyFavouritedException : Exception {
    public AlreadyFavouritedException() : base("You cannot favourite the same recipe twice !")
    {}
}