namespace RecipeAppTest.searcher;

using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

[TestClass]

public class SearchByRatingTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RatingBelow0Exception(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        int rating = -1;
        
        //Act
        SearchByRating searcher = new(context, rating);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RatingTooHighAbove5(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        int rating = 6;
        
        //Act
        SearchByRating searcher = new(context, rating);
    }

    [TestMethod]
    public void RatingSearchReturnsCorrectList(){
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
        List<Rating> ratings1 = new List<Rating> {
            new(3, "gross", new()),
            new(4, "diarrhea inducing", new()),
        };
        List<Rating> ratings2 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };
        List<Rating> ratings3 = new List<Rating> {
            new(5, "diarrhea inducing", new()),
            new(4, "meh, im vegan", new())
        };
        List<Rating> ratings4 = new List<Rating> {
            new(3, "gross", new()),
            new(5, "diarrhea inducing", new())
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 5, ings1, steps, ratings0, new()),
            new ("Recipe1", user, "desc", 5, ings4, steps, ratings1, new()),
            new ("Recipe2", user, "desc", 5, ings3, steps, ratings2, new()),
            new ("Recipe3", user, "desc", 5, ings2, steps, ratings3, new()),
            new ("Recipe4", user, "desc", 5, ings1, steps, ratings4, new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByRating searcher = new(mockContext.Object, 3);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(2,filteredList.Count);
        Assert.AreEqual("Recipe1", filteredList[0].Name);
        Assert.AreEqual("Recipe4", filteredList[1].Name);
    }

    [TestMethod]
    public void RatingSearchReturnsEmptyList(){
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
        List<Rating> ratings1 = new List<Rating> {
            new(3, "gross", new()),
            new(4, "diarrhea inducing", new()),
        };
        List<Rating> ratings2 = new List<Rating> {
            new(4, "amazing", new()),
            new(5, "fantastic", new())
        };
        List<Rating> ratings3 = new List<Rating> {
            new(5, "diarrhea inducing", new()),
            new(4, "meh, im vegan", new())
        };
        List<Rating> ratings4 = new List<Rating> {
            new(3, "gross", new()),
            new(5, "diarrhea inducing", new())
        };

        var listData = new List<Recipe>
        {
            new ("Recipe0", user, "desc", 5, ings1, steps, ratings0, new()),
            new ("Recipe1", user, "desc", 5, ings4, steps, ratings1, new()),
            new ("Recipe2", user, "desc", 5, ings3, steps, ratings2, new()),
            new ("Recipe3", user, "desc", 5, ings2, steps, ratings3, new()),
            new ("Recipe4", user, "desc", 5, ings1, steps, ratings4, new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByRating searcher = new(mockContext.Object, 2);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(0,filteredList.Count);
    }

}