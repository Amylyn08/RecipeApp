using System.Dynamic;

namespace RecipeApp.Models;

public class Rating {
     public int __stars{get; private set;}
     public string __description{get; private set;}
     public User __user{get; private set;}

    public Rating(int stars, string desc, User user){
        if (stars > 5 || stars < 0) throw new ArgumentException("Stars must be between 0 to 5");
        if(user == null) throw new ArgumentException("User cannot be null");
        if(desc.Length > 512) throw new ArgumentException("Max description length exceeded!");
        if(desc == null) desc = "";

        __stars = stars;
        __description = desc;
        __user = user;
    }
}