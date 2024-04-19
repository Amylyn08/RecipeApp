namespace RecipeApp.Searcher;

using RecipeApp.Context;
using RecipeApp.Models;

public class SearchByRating : SearcherBase{

    private readonly int _criteria;

    /// <summary>
    /// Constructor taking assigning the criteria, corresponding to the num ratings specified.
    /// </summary>
    /// <param name="criteria">Rating/stars</param>
    public SearchByRating(int rating)  {
        if (rating < 0 || rating > 5) 
            throw new ArgumentException("Rating must be between 1 and 5");
        _criteria = rating;
    }

    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}