using RecipeApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace RecipeApp.Api;

// maps api response
public class ApiResponse {
    public string name {get; set;}
    public double calories {get; set;}
    public double serving_size_g {get; set;}
    public double fat_total_g {get; set;}
    public double fat_saturated_g {get; set;}
    public double protein_g {get; set;}
    public double sodium_mg {get; set;}
    public double potatssium_mg {get; set;}
    public double cholesterol_mg {get; set;}
    public double carbohydrates_total_g {get; set;}
    public double fiber_g {get; set;}
    public double sugar_g {set; get;}
}

public class NutritionFactFetcher {
    public static async void FetchForAllIngredients(List<Ingredient> ingredients) {
        List<ApiResponse> apiResponse = new();
        foreach (Ingredient ingredient in ingredients) {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", "PeQs6o8bXVEeTdvFd/eVHg==3n01cl0udTmgRJTD"); 
            var response = client.GetAsync($"https://api.calorieninjas.com/v1/nutrition?query={ingredient.Quantity} {ingredient.Name}").Result;
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(responseBody);
            System.Console.WriteLine(responseBody.Split(":")[1]);
        }
    }
}