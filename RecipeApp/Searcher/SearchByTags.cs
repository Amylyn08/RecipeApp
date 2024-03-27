namespace RecipeAppTest.searcher;

using RecipeApp.Models;
using RecipeApp.Searcher;

public class SearchByTags :ISearcher{

    private readonly string _criteria;

    /// <summary>
    /// Contructor for SearchByTags taking in tagName specified.
    /// </summary>
    /// <param name="tagName">The tag name specified.</param>
    public SearchByTags(string tagName) {
        if (tagName == null)
            throw new ArgumentException("Tag name cannot be null");
        if (tagName.Length == 0)
            throw new ArgumentException("Tag name cannot be empty");
        _criteria = tagName;
    }

    /// <summary>
    /// Gets list of recipes containing the tagName specified. 
    /// </summary>
    /// <param name="recipes">The list of recipes that are being used.</param>
    /// <returns>List of filtered Recipse containing tag name specified.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            foreach(Tag t in TagsOfRecipe(r)){
                if(t.TagName.Contains(_criteria, StringComparison.OrdinalIgnoreCase)){
                    filteredRecipes.Add(r);
                }
            }
        }
        return filteredRecipes;
    }

    /// <summary>
    /// Gets the List of tags from a recipe.
    /// </summary>
    /// <param name="r">The specified recipe.</param>
    /// <returns>List of Tags.</returns>
    private static List<Tag> TagsOfRecipe (Recipe r){
        return r.Tags;
    }
}