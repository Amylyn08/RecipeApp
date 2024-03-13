using System.Dynamic;

namespace RecipeApp.Models;
using RecipeApp.Constants;

/// <summary>
/// For rating of recipes, contains stars, description, and user/author of the rating.
/// </summary>
public class Rating {
     public int __stars{get; private set;}
     public string __description{get; private set;}
     public User __user{get; private set;}

/// <summary>
/// Constructor of Rating.
/// </summary>
/// <param name="stars">Stars for rating out of 5</param>
/// <param name="desc">Dscription of the rating</param>
/// <param name="user">User who created the rating</param>
/// <exception cref="ArgumentException">Throws exceptions if user disrespects contraints</exception>
    public Rating(int stars, string desc, User user){
        if (stars > 5 || stars < 0) throw new ArgumentException("Stars must be between 0 to 5");
        if(user == null) throw new ArgumentException("User cannot be null");
        if(desc.Length > Constants.MAX_DESCRIPTION_LENGTH) throw new ArgumentException("Max description length exceeded!");
        if(desc == null) desc = "";

        __stars = stars;
        __description = desc;
        __user = user;
    }
}