using RecipeApp.Api;
using RecipeApp.Models;

namespace RecipeAppTest.Api;

[TestClass]
public class NutritionFactFetcherTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void FetchNullRecipeThrowsArgumentException() {
        // Arrange
        NutritionFactFetcher nutritionFactFetcher = new NutritionFactFetcher();
        // Act
        nutritionFactFetcher.Fetch(null);
    }

    [TestMethod]
    public void FetchRecipeReceivesRespoonse() {
        // Arrange
        NutritionFactFetcher nutritionFactFetcher = new NutritionFactFetcher();
        List<Ingredient> ingredients = new List<Ingredient>() {
            new Ingredient("Potato", 2, UnitOfMeasurement.AMOUNT, 20)
        };
        List<Step> steps = new List<Step>() {
            new Step(5, "Do potato")
        };
        User user = new User("Rida", "This is rida", "This is my password", new(), new(), "Salt");
        Recipe recipe = new Recipe("Potato", user, "Potato", 2, ingredients, steps, new(), new());
        // Act
        NutritionResponse apiResponse = (NutritionResponse) nutritionFactFetcher.Fetch(recipe);
        // Assert
        Assert.IsNotNull(apiResponse);
        Assert.IsTrue(apiResponse.calories > 0);
    }    
}