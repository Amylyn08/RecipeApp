using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByServingsTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ServingsInputBelowMinimum(){
        //Arrange
        int servings = 0;
        SplankContext context = new();

        //Act
        SearchByServings searcher = new(context, servings);
    }

    [TestMethod]
    public void SearchByServingsCorrectList(){
        //Arrange
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings2 = new() {
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings3 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings4 = new() {
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };

        List<Step> steps = new() {
            new Step(10, "eat")
        };

        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, steps, ratings0, new()),
            new ("Recipe1", user, "desc", 1, ings4, steps, ratings0, new()),
            new ("Recipe2", user, "desc", 6, ings3, steps, ratings0, new()),
            new ("Recipe3", user, "desc", 2, ings2, steps, ratings0, new()),
            new ("Recipe4", user, "desc", 2, ings1, steps, ratings0, new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByServings searcher = new(mockContext.Object, 2);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(3, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe3", filteredList[1].Name);
        Assert.AreEqual("Recipe4", filteredList[2].Name);
    }
    [TestMethod]
    public void SearchByServingsReturnsEmptyList(){
        //Arrange
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings2 = new() {
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings3 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        List<Ingredient> ings4 = new() {
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };

        List<Step> steps = new() {
            new Step(10, "eat")
        };

        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, steps, ratings0, new()),
            new ("Recipe1", user, "desc", 1, ings4, steps, ratings0, new()),
            new ("Recipe2", user, "desc", 6, ings3, steps, ratings0, new()),
            new ("Recipe3", user, "desc", 2, ings2, steps, ratings0, new()),
            new ("Recipe4", user, "desc", 2, ings1, steps, ratings0, new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByServings searcher = new(mockContext.Object, 3);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(0, filteredList.Count);
    }


}