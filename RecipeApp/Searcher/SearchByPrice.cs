using RecipeApp.Searcher;

namespace RecipeApp.Models;

public class SearchByPrice :ISearcher{

    private int minPrice;
    private int maxPrice;

    public SearchByPrice(int min, int max){
        minPrice = min;
        maxPrice = max;
    }

    //implement!!!
    public List<Recipe> FilterRecipes (List<Recipe> recipes){
        List<Recipe> filteredRecipes = new ();
        return filteredRecipes;
    }

}