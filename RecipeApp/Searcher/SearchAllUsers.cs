using RecipeApp.Context;
using RecipeApp.Models;

namespace RecipeApp.Searcher;

public class SearchAllUsers :SearcherBase{

    private readonly string _criteria;

    public SearchAll(SplankContext context, string username) : base(context){
        if (username == null) throw new ArgumentException("Username cannot be null");
        if (username.Length == 0) throw new ArgumentException("Tag name cannot be empty");
        
    }

    public override List<Recipe> FilterRecipes(){

    }
}