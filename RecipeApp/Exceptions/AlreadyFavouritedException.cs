namespace RecipeApp.Exceptions;

public class AlreadyFavouritedException : Exception {
    public AlreadyFavouritedException() : base("You cannot favourite the same recipe twice !")
    {}
}