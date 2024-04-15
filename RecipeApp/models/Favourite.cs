namespace RecipeApp.Models;

public class Favourite {
    public int FavouriteId { get; set; }
    public Recipe Recipe { get; set; }
    public User User { get; set; }
}