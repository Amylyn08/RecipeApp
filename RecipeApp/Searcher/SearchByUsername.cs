namespace RecipeApp.Searcher;

using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;


public class SearchByUsername : SearcherBase{

    private readonly string _criteria;

    /// <summary>
    /// Constructor for SearchByUsername takes in username
    /// </summary>
    /// <param name="username">Name of user</param>
    public SearchByUsername(DbContext context, string username) : base(context) {
        if (username == null)
            throw new ArgumentException("Username cannot be null");
        if (username.Length == 0)
            throw new ArgumentException("Username cannot be empty");
        _criteria = username;
    }


    public override List<Recipe> FilterRecipes()
    {
        throw new NotImplementedException();
    }
}