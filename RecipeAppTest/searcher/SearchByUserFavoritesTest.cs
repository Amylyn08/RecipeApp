using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;
[TestClass]
public class SearchByUserFavoritesTest
{

    [TestMethod]
    public void SearchByFavsReturnsCorrectList()
    {   
        
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

        Recipe recipe = new Recipe("Recipe0", user, "desc", 2, ings1, steps, ratings0, new());
        Recipe recipe1 = new Recipe("Recipe1", user, "desc", 2, ings1, steps, ratings0, new());
        Recipe recipe2 = new Recipe("Recipe2", user, "desc", 2, ings1, steps, ratings0, new());
        Recipe recipe3 = new Recipe("Recipe3", user, "desc", 2, ings1, steps, ratings0, new());
        Recipe recipe4 = new Recipe("Recipe4", user, "desc", 2, ings1, steps, ratings0, new());

        User user1 = new("Amy", "human", "amyhuman123", new(), new(), "meow");
        User user2 = new("Prabhjot", "human", "amyhuman123", new(), new(), "meow");
        User user3 = new("Rida", "human", "amyhuman123", new(), new(), "meow");
        var listData = new List<Favourite>(){
            new(recipe, user1),
            new(recipe1, user1),
            new(recipe2, user1),
            new(recipe3, user2),
            new(recipe4, user3),

        };
        //Arrange
        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<Favourite>>();
        mockSet.As<IQueryable<Favourite>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Favourite>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Favourite>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Favourite>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Favourites).Returns(mockSet.Object);
        SearchByUserFavorite searcher = new(mockContext.Object, user1);

        //Act
        List<Recipe> filteredList = searcher.FilterRecipes();

        //Asert
        Assert.AreEqual(3, filteredList.Count);
        Assert.AreEqual("Recipe0", filteredList[0].Name);
        Assert.AreEqual("Recipe1", filteredList[1].Name);
        Assert.AreEqual("Recipe2", filteredList[2].Name);
    }
}