using System.Dynamic;

namespace RecipeApp.Models;

public class Rating {
     private int __stars;
     private string __description;
     private string __user;

    public int Stars
    {
        get => __stars;
        set => __stars = value;
    }   
    public string Description
    {
        get => __description;
        set => __description = value;
    }   
    public string User
    {
        get => __user;
        set => __user = value;
    }   

    public Rating(int stars, string desc, string user){
        if (stars > 5 || stars < 0) throw new ArgumentException("Stars must be between 0 to 5");
        if(stars == null) throw new ArgumentException("Stars cannot be null");
        if(user == null) throw new ArgumentException("User cannot be null");
        if(desc == null) desc = "";

        __stars = stars;
        __description = desc;
        __user = user;
    }
}