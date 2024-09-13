namespace RecipeApp.Api;

/// <summary>
/// JSON representation of the API used to fetch nutrition responses
/// </summary>
public class NutritionResponse : ApiResponse {
    public double calories {get; set;} 
    public double fat_total_g {get; set;}
    public double fat_saturated_g {get; set;}
    public double protein_g {get; set;}
    public double sodium_mg {get; set;}
    public double cholesterol_mg {get; set;} 
    public double carbohydrates_total_g {get; set;}
    public double fiber_g {get; set;}
    public double sugar_g {set; get;}

    public override string ToString()
    {
        return $"Calories: {this.calories} \n Fats: {this.fat_total_g} \n Saturated Fat: {this.fat_saturated_g} \n Protein: {this.protein_g} \n Sodium {this.sodium_mg} \n Cholestetol {this.cholesterol_mg} \n Carbs: {this.carbohydrates_total_g} \n Fiber: {this.fiber_g} \n Sugar: {this.sugar_g}";
    }
}