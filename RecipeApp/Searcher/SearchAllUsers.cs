using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Searcher;

public class SearchAllUsers
{

    private readonly string _criteria;
    private SplankContext _context;

    /// <summary>
    /// Constructor for searchAllUsers, validates username, assigns username
    /// and context.
    /// </summary>
    /// <param name="context">The splank context</param>
    /// <param name="username">the username of specified by input.</param>
    /// <exception cref="ArgumentException"></exception>
    public SearchAllUsers(SplankContext context, string username)
    {
        if (username == null) throw new ArgumentException("Username cannot be null");
        if (username.Length == 0) throw new ArgumentException("Tag name cannot be empty");
        _criteria = username;
        _context = context;
    }

    public List<User> GetUserByName()
    {
        List<User> userFiltered = _context.Users
                                .Where(user => user.Name.ToLower().Contains(_criteria.ToLower()))
                                .ToList<User>();
        return userFiltered;
    }

}