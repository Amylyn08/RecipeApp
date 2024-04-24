using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using Moq;
using Microsoft.EntityFrameworkCore;
namespace RecipeAppTest.Services;

[TestClass]
public class RatingServiceTest {


    [TestMethod]
    public void AddRatingSuccessful() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe = new Recipe("Recipe1", user, "rida was here", 10, ings, steps, new(), new());
        Rating rating = new Rating(5, "this is amazing", user);

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RatingService ratingService = new(mockContext.Object);

        //Act
        ratingService.RatingRecipe(rating, recipe);
        //Assert
        mockContext.Verify(m => m.Update(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddRatingNull_ThrowsException() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe = new Recipe("Recipe1", user, "rida was here", 10, ings, steps, new(), new());
        Rating rating = null!;

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RatingService ratingService = new(mockContext.Object);

        //Act
        ratingService.RatingRecipe(rating, recipe);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddRatingNullRecipe_ThrowsException() {
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };
        Recipe recipe = null!;
        Rating rating = new Rating(5, "this is amazing", user);

        var mockSet = new Mock<DbSet<Recipe>>();

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        RatingService ratingService = new(mockContext.Object);

        //Act
        ratingService.RatingRecipe(rating, recipe);
    }
}