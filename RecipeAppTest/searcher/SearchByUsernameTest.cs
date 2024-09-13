using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByUsernameTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UsernameIsNullException(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        string username = null!;

        //Act
        SearchByUsername searcher = new(context, username);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserNameLengthIsZeroException(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        string username = "";

        //Act
        SearchByUsername searcher = new(context, username);
    }

    [TestMethod]
    public void SearchByUsernameReturnsCorrectList(){
         //Arrange
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        User user = new User("Rida", "I am rida 2", "RidaPassword",  new(), "randomsalt");
        User user1 = new User("Amy", "I am rida 2", "RidaPassword",  new(), "randomsalt");
        User user2= new User("Prabhjot", "I am rida 2", "RidaPassword", new(), "randomsalt");
        User user3 = new User("Bianca", "I am rida 2", "RidaPassword",  new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user1, "desc", 2, ings1, steps, ratings0, new()),
            new ("Recipe1", user1, "desc", 1, ings1, steps, ratings0, new()),
            new ("Recipe2", user2, "desc", 6, ings1, steps, ratings0, new()),
            new ("Recipe3", user3, "desc", 2, ings1, steps, ratings0, new()),
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
        SearchByUsername searcher = new(mockContext.Object, "Amy");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(2, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe1", filteredList[1].Name);
    }

    [TestMethod]
    public void SearchByUsernameReturnsEmptyList(){
         //Arrange
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
        User user = new User("Rida", "I am rida 2", "RidaPassword", new(), "randomsalt");
        User user1 = new User("Amy", "I am rida 2", "RidaPassword",  new(), "randomsalt");
        User user2= new User("Prabhjot", "I am rida 2", "RidaPassword",  new(), "randomsalt");
        User user3 = new User("Bianca", "I am rida 2", "RidaPassword",  new(), "randomsalt");

        List<Rating> ratings0 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };

        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user1, "desc", 2, ings1, steps, ratings0, new()),
            new ("Recipe1", user1, "desc", 1, ings1, steps, ratings0, new()),
            new ("Recipe2", user2, "desc", 6, ings1, steps, ratings0, new()),
            new ("Recipe3", user3, "desc", 2, ings1, steps, ratings0, new()),
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
        SearchByUsername searcher = new(mockContext.Object, "James");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Assert
        Assert.AreEqual(0, filteredList.Count);
    }
}