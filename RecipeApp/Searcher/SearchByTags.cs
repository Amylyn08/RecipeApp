namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

public class SearchByTags :SearcherBase{

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
    /// Gets list of recipes where tag name matches the criteria.
    /// </summary>
    /// <returns>Returns filtered list of recipes.</returns>
    public override List<Recipe> FilterRecipes()
    {
        List<Recipe> filteredRecipes = Context.Recipes 
                                    .Where(recipe => recipe.Tags.Any(tag => 
                                    tag.TagName.Equals(_criteria)))
                                    .ToList<Recipe>();
        return filteredRecipes;
    }
}