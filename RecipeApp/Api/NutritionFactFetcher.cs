using RecipeApp.Exceptions;
using RecipeApp.Models;
using System.Text.Json;

namespace RecipeApp.Api;

public class NutritionFactFetcher : IApiForIngredients {
    public string JsonAsString { get; private set; } = null!;

    // This may or may not work depending on the ingredients
    // Use try/catch in the code that is calling this method and simply 
    // tell the client that the nutrition facts for this recipe was not available
    // if the API call fails
    // The code is ugly, but it works
    public ApiResponse Fetch(Recipe recipe) {
        if (recipe is null) {
            throw new ArgumentException("Recipe cannot be null");
        }

        // total macros for a recipe
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
            var apiRep = JsonSerializer.Deserialize<NutritionResponse>(fullParsedJsonData);

            if (apiRep is null) {
                throw new ApiException("Could not fetch recipe nutrition facts");
            }

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

    private async void TransformResponseToDeserializableJsonString(HttpResponseMessage httpResponseMessage) {
        JsonAsString = await httpResponseMessage.Content.ReadAsStringAsync();
    }
}