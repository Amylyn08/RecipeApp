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

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InsertRecipeNullThrowsException() {
        //Arrange
        Recipe recipe = null;

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);
        //Act
        recipeService.CreateRecipe(recipe);
    }

    [TestMethod]
    public void DeletingRecipeSuccessful() {
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
        recipeService.DeleteRecipe(recipe);
        //Assert
        mockContext.Verify(m => m.Remove(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteRecipeNullThrowsException() {
        //Arrange
        Recipe recipe = null;

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);
        //Act
        recipeService.DeleteRecipe(recipe);
    }

    [TestMethod]
    public void UpdateRecipeSucceful() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe1 = new Recipe("Recipe1", user, "rida was here", 10, ings, steps, new(), new());
        Recipe recipe2 = new Recipe("potato", user, "rida was here", 10, ings, steps, new(), new());

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);

        //Act
        recipeService.UpdateRecipe(recipe1, recipe2);
        //Assert
        mockContext.Verify(m => m.Update(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipe_OldRecipeNull_ThrowsException() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe1 = null;
        Recipe recipe2 = new Recipe("potato", user, "rida was here", 10, ings, steps, new(), new());

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);

        //Act
        recipeService.UpdateRecipe(recipe1, recipe2);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipe_NewRecipeNull_ThrowsException() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe1 = new Recipe("Recipe1", user, "rida was here", 10, ings, steps, new(), new());
        Recipe recipe2 = null;

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RecipeService recipeService = new(mockContext.Object);

        //Act
        recipeService.UpdateRecipe(recipe1, recipe2);

    }
}