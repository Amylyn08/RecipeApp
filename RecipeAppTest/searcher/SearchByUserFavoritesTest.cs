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
        // //Arrange
        // List<Ingredient> ings1 = new() {
        //     new Ingredient("caramel", 5, UnitOfMeasurement.AMOUNT, 20.2)
        // };

        // List<Step> steps = new() {
        //     new Step(10, "eat")
        // };
        // User user = new User("Rida2", "I am rida 2", "RidaPassword", new(), new(), "randomsalt");

        // List<Rating> ratings0 = new List<Rating> {
        // new(4, "amazing", new()),
        // new(5, "fantastic", new())
        // };

        // List<Recipe> favList1 = new List<Recipe>{
        //     new ("Salad", user, "desc", 2, ings1, steps, ratings0, new()),
        //     new ("BTL", user, "desc", 1, ings1, steps, ratings0, new()),
        //     new ("Paneer", user, "desc", 1, ings1, steps, ratings0, new()),
        // };
        // List<Recipe> favList2 = new List<Recipe>{
        //     new ("Hamburger", user, "desc", 2, ings1, steps, ratings0, new()),
        //     new ("Sharwma", user, "desc", 1, ings1, steps, ratings0, new()),
        // };
        // List<Recipe> favList3 = new List<Recipe>{
        //     new ("Shish Taouk", user, "desc", 2, ings1, steps, ratings0, new()),
        //     new ("Chick Pea Salad", user, "desc", 1, ings1, steps, ratings0, new()),
        // };
        // List<Recipe> favList4 = new List<Recipe>{
        //     new ("Tofu Stir Fry", user, "desc", 2, ings1, steps, ratings0, new()),
        //     new ("Lo Mein", user, "desc", 1, ings1, steps, ratings0, new()),
        // };
        // List<Recipe> favList5 = new List<Recipe>{
        //     new ("Wonton Soup", user, "desc", 2, ings1, steps, ratings0, new()),
        //     new ("Xiu mai", user, "desc", 1, ings1, steps, ratings0, new()),
        // };

        // var listData = new List<User>
        // {
        //     new("Amy", "student", "AmyStudent", favList1, new(), "meow"),
        //     new("Jeffrey", "student", "AmyStudent", favList2, new(), "meow"),
        //     new("Michael", "student", "AmyStudent", favList3, new(), "meow"),
        //     new("Prabhjot", "student", "AmyStudent", favList4, new(), "meow"),
        //     new("Rida", "student", "AmyStudent", favList5, new(), "meow")
        // };

        // User amy = new("Amy", "student", "AmyStudent", favList1, favList1, "hi");

        // var data = listData.AsQueryable();

        // var mockSet = new Mock<DbSet<Recipe>>();
        // mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        // mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        // mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        // mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        // var mockContext = new Mock<SplankContext>();
        // mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        // SearchByUserFavorite searcher = new(mockContext.Object, amy);

        // //Act
        // List<Recipe> filteredList = searcher.FilterRecipes();

        // //Assert
        // Assert.AreEqual(3, filteredList.Count);
        // Assert.AreEqual("Salad", filteredList[0].Name);
        // Assert.AreEqual("BLT", filteredList[1].Name);
        // Assert.AreEqual("Paneer", filteredList[2].Name);
    }
}