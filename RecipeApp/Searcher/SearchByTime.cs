namespace RecipeAppTest.searcher;

using RecipeApp.Models;
using RecipeApp.Searcher;

public class SearchByTime : ISearcher{
    private int minTime;
    private int maxTime;

    /// <summary>
    /// Constructor for SearchByTime using time in minutes specified.
    /// </summary>
    /// <param name="timeInMinutes">The time in minutes specified.</param>
    public SearchByTime(int min, int max){
        minTime = min;
        maxTime = max;
    }

    /// <summary>
    /// Gets filtered list of recipes containing the time in minute of step specified.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns>The filtered list of recipes that take up the amount of time specified.</returns>
    public List<Recipe> FilterRecipes(List<Recipe> recipes){
        List<Recipe> filteredRecipes = new();
        foreach(Recipe r in recipes){
            foreach(Step step in StepsInRecipe(r)){
                    if(step.TimeInMinutes >= minTime && step.TimeInMinutes <=maxTime){
                        filteredRecipes.Add(r);
                    }
                }
            }
        return filteredRecipes;
    }

    /// <summary>
    /// Gets list of steps for a recipe
    /// </summary>
    /// <param name="r">The recipe being used</param>
    /// <returns>List of steps for recipe</returns>
    private List<Step> StepsInRecipe (Recipe r){
        return r.Steps;
    }

}