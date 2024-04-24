using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByKeyWordTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void keywordNullException(){
        //Arrange
        SplankContext context = new();
        string keyword = null;

        //Act
        SearcherBase searcher = new SearchKeyWord(context, keyword);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void keywordNullLength0Exception(){
        //Arrange
        SplankContext context = new();
        string keyword = "";

        //Act
        SearcherBase searcher = new SearchKeyWord(context, keyword);
    }

    [TestMethod]
    public void KeyWordSearchCorrect(){
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings0 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
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

        var listData = new List<Recipe>
        {
            new Recipe("Recipe0", user, "This is the best chicken sandwich ever.", 5,
                         ings0, steps, new(), new()),
            new Recipe("Recipe1", user, "An authentic lebanese chicken shish taouk.",
                         5, ings1, steps, new(), new()),
            new Recipe("Recipe2", user, "A vegan dish with tofu and mushrooms", 
                        5, ings2, steps, new(), new()),
            new Recipe("Recipe3", user, "A low calorie beef burger", 
                        5, ings3, steps, new(), new()),
            new Recipe("Recipe4", user, "A high calorie ribs recipe with potatoes.",
                         5, ings4, steps, new(), new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchKeyWord searcher = new(mockContext.Object, "chicken");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(2,filteredList.Count);
        Assert.IsTrue(filteredList[0].Description.Contains("chicken"));
        Assert.IsTrue(filteredList[1].Description.Contains("chicken"));
    }


    [TestMethod]
    public void KeyWordSearchEmptyList(){
        //Arrange
        User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Ingredient> ings0 = new() {
            new Ingredient("potato", 5, UnitOfMeasurement.AMOUNT, 20.2)
        };
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

        var listData = new List<Recipe>
        {
            new Recipe("Recipe0", user, "This is the best chicken sandwich ever.", 5,
                         ings0, steps, new(), new()),
            new Recipe("Recipe1", user, "An authentic lebanese chicken shish taouk.",
                         5, ings1, steps, new(), new()),
            new Recipe("Recipe2", user, "A vegan dish with tofu and mushrooms", 
                        5, ings2, steps, new(), new()),
            new Recipe("Recipe3", user, "A low calorie beef burger", 
                        5, ings3, steps, new(), new()),
            new Recipe("Recipe4", user, "A high calorie ribs recipe with potatoes.",
                         5, ings4, steps, new(), new())
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        SearchKeyWord searcher = new(mockContext.Object, "carrots");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(0,filteredList.Count);
    }
}