namespace RecipeApp.Models;

public interface ISearcher{

    /// <summary>
    /// Returns a list of recipes matching a filter.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    List<Recipe> FilterRecipes(List<Recipe> recipes);
}