using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByTagsTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TagIsNullException(){
        //Arrange
        string tagName = null!;
        SplankContext context = SplankContext.GetInstance();
        
        //Act
        SearchByTags searcher = new(context, tagName);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TagIsZeroLengthException(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        string tagName = "";

        //Act
        SearchByTags searcher = new(context, tagName);
    }

    [TestMethod]
    public void SearchByTagNameReturnsCorrectList(){
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

        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        List<Tag> tags0 = new List<Tag> {
            new("beef"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags1 = new List<Tag> {
            new("beef"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags2 = new List<Tag> {
            new("goat"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags3 = new List<Tag> {
            new("chicken"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags4 = new List<Tag> {
            new("lamb"),
            new("healthy"),
            new("low-calorie"),
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, steps, ratings0, tags0),
            new ("Recipe1", user, "desc", 1, ings4, steps, ratings0, tags1),
            new ("Recipe2", user, "desc", 6, ings3, steps, ratings0, tags2),
            new ("Recipe3", user, "desc", 2, ings2, steps, ratings0, tags3),
            new ("Recipe4", user, "desc", 2, ings1, steps, ratings0, tags4)
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByTags searcher = new(mockContext.Object, "beef");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(2, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe1", filteredList[1].Name);
    }
    [TestMethod]
    public void SearchByTagNameReturnsEmptyList(){
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

        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        List<Tag> tags0 = new List<Tag> {
            new("beef"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags1 = new List<Tag> {
            new("beef"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags2 = new List<Tag> {
            new("beef"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags3 = new List<Tag> {
            new("chicken"),
            new("healthy"),
            new("low-calorie"),
        };
        List<Tag> tags4 = new List<Tag> {
            new("lamb"),
            new("healthy"),
            new("low-calorie"),
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, steps, ratings0, tags0),
            new ("Recipe1", user, "desc", 1, ings4, steps, ratings0, tags1),
            new ("Recipe2", user, "desc", 6, ings3, steps, ratings0, tags2),
            new ("Recipe3", user, "desc", 2, ings2, steps, ratings0, tags3),
            new ("Recipe4", user, "desc", 2, ings1, steps, ratings0, tags4)
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByTags searcher = new(mockContext.Object, "salmon");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(0, filteredList.Count);
    }
}