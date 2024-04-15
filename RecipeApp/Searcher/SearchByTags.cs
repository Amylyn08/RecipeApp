namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

public class SearchByTags :SearcherBase{

    private readonly string _criteria;

    /// <summary>
    /// Contructor for SearchByTags taking in tagName specified.
    /// </summary>
    /// <param name="tagName">The tag name specified.</param>
    public SearchByTags(DbContext context ,string tagName) : base(context) {
        if (tagName == null)
            throw new ArgumentException("Tag name cannot be null");
        if (tagName.Length == 0)
            throw new ArgumentException("Tag name cannot be empty");
        _criteria = tagName;
    }

    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}