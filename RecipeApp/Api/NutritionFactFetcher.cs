using RecipeApp.Exceptions;
using RecipeApp.Models;
using System.Text.Json;

namespace RecipeApp.Api;

/// <summary>
/// Fetches nutrition facts about a recipe through an API
/// </summary>
public class NutritionFactFetcher : IApiForIngredients {
    public string JsonAsString { get; private set; } = null!;

    /// <summary>
    /// Gets nutrition facts
    /// </summary>
    /// <param name="recipe">Recipe to get nutrition facts from</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If recipe is null</exception>
    /// <exception cref="ApiException">If API call fails</exception>
    public ApiResponse Fetch(Recipe recipe) {
        if (recipe is null) {
            throw new ArgumentException("Recipe cannot be null");
        }

        var totalCalories = 0.0;
        var totalFat = 0.0;
        var totalSaturatedFat = 0.0;
        var totalProtein = 0.0;
        var totalSodium = 0.0;
        var totalCholes = 0.0;
        var totalCarbs = 0.0;
        var totalFiber = 0.0;
        var totalSugar = 0.0;
        
        foreach (var ingredient in recipe.Ingredients) {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", "PeQs6o8bXVEeTdvFd/eVHg==3n01cl0udTmgRJTD"); 
            var response = client.GetAsync($"https://api.calorieninjas.com/v1/nutrition?query={ingredient.Quantity} {ingredient.UnitOfMeasurement} {ingredient.Name}").Result;
            
            TransformResponseToDeserializableJsonString(response); // KEEP THIS PLS

            var halfParsedData = JsonAsString.Substring(JsonAsString.IndexOf("[{") + 1);
            var fullParsedJsonData = halfParsedData.Substring(0, halfParsedData.IndexOf("}") + 1);
            var apiRep = JsonSerializer.Deserialize<NutritionResponse>(fullParsedJsonData) ?? throw new ApiException("Could not fetch recipe nutrition facts");
            totalCalories += apiRep.calories;
            totalFat += apiRep.fat_total_g;
            totalSaturatedFat += apiRep.fat_saturated_g;
            totalProtein += apiRep.protein_g;
            totalSodium += apiRep.sodium_mg;
            totalCholes += apiRep.cholesterol_mg;
            totalCarbs += apiRep.carbohydrates_total_g;
            totalFiber += apiRep.fiber_g;
            totalSugar += apiRep.sugar_g;
        }

        return new NutritionResponse() {
            calories = totalCalories,
            fat_total_g = totalFat,
            fat_saturated_g = totalSaturatedFat,
            protein_g = totalProtein,
            sodium_mg = totalSodium,
            cholesterol_mg = totalCholes,
            carbohydrates_total_g = totalCarbs,
            fiber_g = totalFiber,
            sugar_g = totalSugar
        };
    }

    /// <summary>
    /// Turns a HTTP response into a JSON string 
    /// </summary>
    /// <param name="httpResponseMessage"></param>
    private async void TransformResponseToDeserializableJsonString(HttpResponseMessage httpResponseMessage) {
        JsonAsString = await httpResponseMessage.Content.ReadAsStringAsync();
    }
}