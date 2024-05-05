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
    public void PriceValueLessThan0(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        double price = -0.1;
        //Act
        SearcherBase searcher = new SearchByPriceRange(context, price);
    }     

    [TestMethod]
    public void PriceRangeSearcherReturnsCorrect(){
    //Arrange
        User user = new("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listDataIngredients = new List<Ingredient>()
        {
            new("potato", 5, UnitOfMeasurement.AMOUNT, 20.2), //0
            new("onion", 5, UnitOfMeasurement.AMOUNT, 10), //1
            new("beef", 5, UnitOfMeasurement.AMOUNT, 50), //2
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 5.2), //3
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 2.2), //4
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 1.2), //5
            new("apples", 5, UnitOfMeasurement.AMOUNT, 2.2), //6
            new("apples", 5, UnitOfMeasurement.AMOUNT, 0.5), //7 
            new("apples", 5, UnitOfMeasurement.AMOUNT, 6), //8
            new("potato", 5, UnitOfMeasurement.AMOUNT, 20.2), //9 
            new("potato", 5, UnitOfMeasurement.AMOUNT, 30.2), //10
            new("potato", 5, UnitOfMeasurement.AMOUNT, 10.2), //11
            new("bread", 5, UnitOfMeasurement.AMOUNT, 19.99), //12
            new("bread", 5, UnitOfMeasurement.AMOUNT, 41.99), //13
            new("bread", 5, UnitOfMeasurement.AMOUNT, 10.99), //14
        };
        //price.total = 5 - 15
       var ings0 = new List<Ingredient>() { listDataIngredients[0], listDataIngredients[1], listDataIngredients[2] }; //80.2
        var ings1 = new List<Ingredient>() { listDataIngredients[3], listDataIngredients[4], listDataIngredients[5] }; //8.6 YES
        var ings2 = new List<Ingredient>() { listDataIngredients[6], listDataIngredients[7], listDataIngredients[8] }; // 8.7 YES
        var ings3 = new List<Ingredient>() { listDataIngredients[9], listDataIngredients[10], listDataIngredients[11] }; // 60.2 NO
        var ings4 = new List<Ingredient>() { listDataIngredients[12], listDataIngredients[13], listDataIngredients[14] }; //72.97 NO

        var listData = new List<Recipe>
        {
            new("Recipe0", user, "desc", 5, ings0, steps, new(), new()) { RecipeId = 0 }, 
            new("Recipe1", user, "desc", 5, ings1, steps, new(), new()) { RecipeId = 2 }, //yes
            new("Recipe2", user, "desc", 5, ings2, steps, new(), new()) { RecipeId = 3 }, //yes
            new("Recipe3", user, "desc", 5, ings3, steps, new(), new()) { RecipeId = 4 },
            new("Recipe4", user, "desc", 5, ings4, steps, new(), new()) { RecipeId = 5 } 
        };

        listDataIngredients[0].RecipeId = 1;
        listDataIngredients[1].RecipeId = 1;
        listDataIngredients[2].RecipeId = 1;
        listDataIngredients[3].RecipeId = 2;
        listDataIngredients[4].RecipeId = 2;
        listDataIngredients[5].RecipeId = 2;
        listDataIngredients[6].RecipeId = 3;
        listDataIngredients[7].RecipeId = 3;
        listDataIngredients[8].RecipeId = 3;
        listDataIngredients[9].RecipeId = 4;
        listDataIngredients[10].RecipeId = 4;
        listDataIngredients[11].RecipeId = 4;
        listDataIngredients[12].RecipeId = 5;
        listDataIngredients[13].RecipeId = 5;
        listDataIngredients[14].RecipeId = 5;


        var data = listData.AsQueryable();
        var ingData = listDataIngredients.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        var ingredientMockSet = new Mock<DbSet<Ingredient>>();

        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.Provider).Returns(ingData.Provider);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.Expression).Returns(ingData.Expression);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.ElementType).Returns(ingData.ElementType);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.GetEnumerator()).Returns(ingData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        mockContext.Setup(m => m.Ingredients).Returns(ingredientMockSet.Object);
        
        SearchByPriceRange searcher = new(mockContext.Object, 10);
        //should get prices from 5 to 15
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
        User user = new("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");
        List<Step> steps = new() {
            new Step(10, "eat")
        };

        var listDataIngredients = new List<Ingredient>()
        {
            new("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new("onion", 5, UnitOfMeasurement.AMOUNT, 10),
            new("beef", 5, UnitOfMeasurement.AMOUNT, 50),
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 5.2),
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new("caramel", 5, UnitOfMeasurement.AMOUNT, 1.2),
            new("apples", 5, UnitOfMeasurement.AMOUNT, 2.2),
            new("apples", 5, UnitOfMeasurement.AMOUNT, 0.5),
            new("apples", 5, UnitOfMeasurement.AMOUNT, 6),
            new("potato", 5, UnitOfMeasurement.AMOUNT, 20.2),
            new("potato", 5, UnitOfMeasurement.AMOUNT, 30.2),
            new("potato", 5, UnitOfMeasurement.AMOUNT, 10.2),
            new("bread", 5, UnitOfMeasurement.AMOUNT, 19.99),
            new("bread", 5, UnitOfMeasurement.AMOUNT, 41.99),
            new("bread", 5, UnitOfMeasurement.AMOUNT, 10.99),
        };

        var ings0 = new List<Ingredient>() { listDataIngredients[0], listDataIngredients[1], listDataIngredients[2] };
        var ings1 = new List<Ingredient>() { listDataIngredients[3], listDataIngredients[4], listDataIngredients[5] };
        var ings2 = new List<Ingredient>() { listDataIngredients[6], listDataIngredients[7], listDataIngredients[8] };
        var ings3 = new List<Ingredient>() { listDataIngredients[9], listDataIngredients[10], listDataIngredients[11] };
        var ings4 = new List<Ingredient>() { listDataIngredients[12], listDataIngredients[13], listDataIngredients[14] };

        var listData = new List<Recipe>
        {
            new("Recipe0", user, "desc", 5, ings0, steps, new(), new()) { RecipeId = 1 },
            new("Recipe1", user, "desc", 5, ings1, steps, new(), new()) { RecipeId = 2 },
            new("Recipe2", user, "desc", 5, ings2, steps, new(), new()) { RecipeId = 3 },
            new("Recipe3", user, "desc", 5, ings3, steps, new(), new()) { RecipeId = 4 },
            new("Recipe4", user, "desc", 5, ings4, steps, new(), new()) { RecipeId = 5 }
        };

        listDataIngredients[0].RecipeId = 1;
        listDataIngredients[1].RecipeId = 1;
        listDataIngredients[2].RecipeId = 1;
        listDataIngredients[3].RecipeId = 2;
        listDataIngredients[4].RecipeId = 2;
        listDataIngredients[5].RecipeId = 2;
        listDataIngredients[6].RecipeId = 3;
        listDataIngredients[7].RecipeId = 3;
        listDataIngredients[8].RecipeId = 3;
        listDataIngredients[9].RecipeId = 4;
        listDataIngredients[10].RecipeId = 4;
        listDataIngredients[11].RecipeId = 4;
        listDataIngredients[12].RecipeId = 5;
        listDataIngredients[13].RecipeId = 5;
        listDataIngredients[14].RecipeId = 5;

  

        var data = listData.AsQueryable();
        var ingData = listDataIngredients.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        var ingredientMockSet = new Mock<DbSet<Ingredient>>();

        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.Provider).Returns(ingData.Provider);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.Expression).Returns(ingData.Expression);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.ElementType).Returns(ingData.ElementType);
        ingredientMockSet.As<IQueryable<Ingredient>>().Setup(m => m.GetEnumerator()).Returns(ingData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        mockContext.Setup(m => m.Ingredients).Returns(ingredientMockSet.Object);
        SearchByPriceRange searcher = new(mockContext.Object, 10000);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();
        //Assert
        Assert.AreEqual(0,filteredList.Count);
    }

}


