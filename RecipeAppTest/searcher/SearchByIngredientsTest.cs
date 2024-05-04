namespace RecipeAppTest.searcher;

using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

[TestClass]

public class SearchByIngredientsTests{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IngredientNameNull_ThrowsException() {
        //Arrange
        string ingName = null!;
        SplankContext context = SplankContext.GetInstance();
        //Act
        SearcherBase searcher = new SearchByIngredients(context, ingName);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IngredientNameEmpty_ThrowsException() {
        //Arrange
        string ingName = "";
        //Act
        SearcherBase searcher = new SearchByIngredients(SplankContext.GetInstance(), ingName);
    }

    [TestMethod]
    public void IngredientsReturnsCorrect(){
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
        SearchByIngredients searcher = new(mockContext.Object, "pOTAto");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(2,filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe3", filteredList[1].Name);
    }

    
    [TestMethod]
    public void IngredientsReturnsEmptyList(){
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
        SearchByIngredients searcher = new(mockContext.Object, "chicken");

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(0,filteredList.Count);
    }




    


}


