using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;

namespace RecipeAppTest.Services;

[TestClass]
public class RecipeServiceTest {
    
    [TestMethod]
    public void InsertRecipeSuccessfull() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe = new Recipe("Recipe1", user, "rida was here", 10, ings, steps, new(), new());

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);
        
        //Act
        recipeService.CreateRecipe(recipe);

        //Assert
        mockContext.Verify(m => m.Add(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
}