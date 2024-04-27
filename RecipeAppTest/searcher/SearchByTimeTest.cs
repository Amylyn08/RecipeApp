using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByTimeTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinBelowZeroException(){
        //Arrange
        SplankContext context = new();
        int min = -1;
        int max = 10;

        //Act
        SearchByTime searcher = new(context, min, max);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MaxBelowZeroException(){
        //Arrange
        SplankContext context = new();
        int min = 2;
        int max = -1;

        //Act
        SearchByTime searcher = new(context, min, max);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinMoreThanMax(){
        //Arrange
        SplankContext context = new();
        int min = 100;
        int max = 10;

        //Act
        SearchByTime searcher = new(context, min, max);
    }

    [TestMethod]
    public void SearchByTimeReturnsCorrectList(){
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

        User user = new("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        List<Rating> ratings0 = new()
        {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        var listStep = new List<Step>() 
        {
            new(5, "eat") { RecipeId = 1 },
            new(10, "eat") { RecipeId = 2 },
            new(30, "eat") { RecipeId = 3 },
            new(25, "eat") { RecipeId = 4 },
            new(75, "eat") { RecipeId = 5 },
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, new() { listStep[0] }, ratings0, new())  { RecipeId = 1 },
            new ("Recipe1", user, "desc", 1, ings4, new() { listStep[1] }, ratings0, new()) { RecipeId = 2 },
            new ("Recipe2", user, "desc", 6, ings3, new() { listStep[2] }, ratings0, new()) { RecipeId = 3 },
            new ("Recipe3", user, "desc", 2, ings2, new() { listStep[3] }, ratings0, new()) { RecipeId = 4 },
            new ("Recipe4", user, "desc", 2, ings1, new() { listStep[4] }, ratings0, new()) { RecipeId = 5 }
        };

        var data = listData.AsQueryable();
        var stepData = listStep.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        var stepMockSet = new Mock<DbSet<Step>>();

        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        stepMockSet.As<IQueryable<Step>>().Setup(m => m.Provider).Returns(stepData.Provider);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.Expression).Returns(stepData.Expression);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.ElementType).Returns(stepData.ElementType);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.GetEnumerator()).Returns(stepData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        mockContext.Setup(m => m.Steps).Returns(stepMockSet.Object);
        SearchByTime searcher = new(mockContext.Object, 5, 10);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Arrange
        Assert.AreEqual(2, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe1", filteredList[1].Name);
    }

    [TestMethod]
    public void SearchByTimeReturnsEmptyList(){
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

        User user = new("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        List<Rating> ratings0 = new()
        {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        var listStep = new List<Step>() 
        {
            new(5, "eat") { RecipeId = 1 },
            new(10, "eat") { RecipeId = 2 },
            new(30, "eat") { RecipeId = 3 },
            new(25, "eat") { RecipeId = 4 },
            new(75, "eat") { RecipeId = 5 },
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, new() { listStep[0] }, ratings0, new())  { RecipeId = 1 },
            new ("Recipe1", user, "desc", 1, ings4, new() { listStep[1] }, ratings0, new()) { RecipeId = 2 },
            new ("Recipe2", user, "desc", 6, ings3, new() { listStep[2] }, ratings0, new()) { RecipeId = 3 },
            new ("Recipe3", user, "desc", 2, ings2, new() { listStep[3] }, ratings0, new()) { RecipeId = 4 },
            new ("Recipe4", user, "desc", 2, ings1, new() { listStep[4] }, ratings0, new()) { RecipeId = 5 }
        };

        var data = listData.AsQueryable();
        var stepData = listStep.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        var stepMockSet = new Mock<DbSet<Step>>();

        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        stepMockSet.As<IQueryable<Step>>().Setup(m => m.Provider).Returns(stepData.Provider);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.Expression).Returns(stepData.Expression);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.ElementType).Returns(stepData.ElementType);
        stepMockSet.As<IQueryable<Step>>().Setup(m => m.GetEnumerator()).Returns(stepData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        mockContext.Setup(m => m.Steps).Returns(stepMockSet.Object);
        SearchByTime searcher = new(mockContext.Object, 1, 4);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Arrange
        Assert.AreEqual(0, filteredList.Count);
    }

}