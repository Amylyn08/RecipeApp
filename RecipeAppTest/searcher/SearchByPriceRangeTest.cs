namespace RecipeAppTest.searcher;

using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

[TestClass]

public class SearchByPriceRangeTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueLessThan0(){
        //Arrange
        SplankContext context = new();
        double min = -0.1;
        double max = 5;

        //Act
        SearcherBase searcher = new SearchByPriceRange(context, min, max);
    }   

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MaxValueLessThan0(){
        //Arrange
        SplankContext context = new();
        double min = 5;
        double max = -0.1;

        //Act
        SearcherBase searcher = new SearchByPriceRange(context, min, max);
    }   

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueMoreThanMax(){
        //Arrange
        SplankContext context = new();
        double min = 10;
        double max = 9;

        //Act
        SearcherBase searcher = new SearchByPriceRange(context, min, max);
    }   

    [TestMethod]
    public void PriceRangeSearcherReturnsCorrect(){
    //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings0 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new Ingredient("onion", 5, UnitOfMeasurement.AMOUNT, 10),
            new Ingredient("beef", 5, UnitOfMeasurement.AMOUNT, 50),
            //80.2 price 
        };
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 5.2),
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 1.2)
            //8.6 price  *
        };
        List<Ingredient> ings2 = new() {
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 0.5),
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 6),
            //8.7 price *

        };
        List<Ingredient> ings3 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 30.2),
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 10.2),
            //60.6 price
        };
        List<Ingredient> ings4 = new() {
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 19.99),
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 41.99),
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 10.99),
            //72.97 price
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listData = new List<Recipe>
        {
            new Recipe("Recipe0", user, "desc", 5, ings0, steps, new(), new()),
            new Recipe("Recipe1", user, "desc", 5, ings1, steps, new(), new()),
            new Recipe("Recipe2", user, "desc", 5, ings2, steps, new(), new()),
            new Recipe("Recipe3", user, "desc", 5, ings3, steps, new(), new()),
            new Recipe("Recipe4", user, "desc", 5, ings4, steps, new(), new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByPriceRange searcher = new(mockContext.Object, 1, 10);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(2,filteredList.Count);
        Assert.AreEqual("Recipe1", filteredList[0].Name);
        Assert.AreEqual("Recipe2", filteredList[1].Name);
    }
    [TestMethod]
    public void PriceRangeSearcherReturnsEmptyList(){
    //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings0 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new Ingredient("onion", 5, UnitOfMeasurement.AMOUNT, 10),
            new Ingredient("beef", 5, UnitOfMeasurement.AMOUNT, 50),
            //80.2 price 
        };
        List<Ingredient> ings1 = new() {
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 5.2),
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 1.2)
            //8.6 price  *
        };
        List<Ingredient> ings2 = new() {
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 0.5),
            new Ingredient("apples", 5, UnitOfMeasurement.AMOUNT, 6),
            //8.7 price *

        };
        List<Ingredient> ings3 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 30.2),
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 10.2),
            //60.6 price
        };
        List<Ingredient> ings4 = new() {
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 19.99),
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 41.99),
            new Ingredient("bread", 5, UnitOfMeasurement.AMOUNT, 10.99),
            //72.97 price
        };
        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listData = new List<Recipe>
        {
            new Recipe("Recipe0", user, "desc", 5, ings0, steps, new(), new()),
            new Recipe("Recipe1", user, "desc", 5, ings1, steps, new(), new()),
            new Recipe("Recipe2", user, "desc", 5, ings2, steps, new(), new()),
            new Recipe("Recipe3", user, "desc", 5, ings3, steps, new(), new()),
            new Recipe("Recipe4", user, "desc", 5, ings4, steps, new(), new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchByPriceRange searcher = new(mockContext.Object, 1, 5);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(0,filteredList.Count);
    }

}


