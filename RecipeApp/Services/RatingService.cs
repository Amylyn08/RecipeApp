using RecipeApp.Models;

namespace RecipeApp.Services;
 

public class RatingService {

    /// <summary>
    /// Rates a recipe
    /// </summary>
    /// <param name="recipeToRate">Recipe thats getting rated</param>
    private static void RatingRecipe(Recipe recipeToRate, User currUser) {
        Rating newRating = CreateRating(currUser);
        recipeToRate.Ratings.Add(newRating);
    }

    /// <summary>
    /// Creates a rating
    /// </summary>
    /// <returns>A Rating made by the user</returns>
    private static Rating CreateRating(User currentUser) {
        Console.WriteLine("How many stars would you like to rate this recipe:");
        int stars = Convert.ToInt32(GetInput());
        Console.WriteLine("Write a review!");
        string description = GetInput();
        return new Rating(stars, description, currentUser);
    }

    private static string GetInput() {
    string input = null;
    do {
        input = Console.ReadLine();
    } while (input == null);
    return input;
    }
}