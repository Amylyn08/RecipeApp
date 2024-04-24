using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByTimeTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UserIsNullObjectException(){
        //Arrange
        User user = null;
        SplankContext context = new();

        //Act
        SearchByUserFavorite searcher = new(context, user);
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

        List<Step> steps = new() {
            new Step(5, "eat")
        };
        List<Step> steps1 = new() {
            new Step(10, "eat")
        };
        List<Step> steps2 = new() {
            new Step(30, "eat")
        };
        List<Step> steps3 = new() {
            new Step(25, "eat")
        };
        List<Step> steps4 = new() {
            new Step(75, "eat")
        };

        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 2, ings1, steps, ratings0, new()),
            new ("Recipe1", user, "desc", 1, ings4, steps1, ratings0, new()),
            new ("Recipe2", user, "desc", 6, ings3, steps2, ratings0, new()),
            new ("Recipe3", user, "desc", 2, ings2, steps3, ratings0, new()),
            new ("Recipe4", user, "desc", 2, ings1, steps4, ratings0, new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByTime searcher = new(mockContext.Object, 5, 10);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Arrange
        Assert.AreEqual(2, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe1", filteredList[1].Name);
    }

}