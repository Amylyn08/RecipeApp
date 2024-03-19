namespace RecipeApp.Models;

public class SearchByServings{

    private int __criteria;

    public SearchByServings(int servings){
        __criteria = servings;
    }

    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new List<Recipe>();
        foreach(Recipe r in recipes){
            if(r.Servings == __criteria){
                filteredRecipes.Add(r);
            }
        }
        return filteredRecipes;
    }
}