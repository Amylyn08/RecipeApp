namespace RecipeApp.Models;

public class SearchByTags{

    private readonly string __criteria;

    /// <summary>
    /// Contructor for SearchByTags taking in tagName specified.
    /// </summary>
    /// <param name="tagName">The tag name specified.</param>
    public SearchByTags(string tagName){
        __criteria = tagName;
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
                if(t.TagName.ToLower().Contains(__criteria.ToLower())){
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