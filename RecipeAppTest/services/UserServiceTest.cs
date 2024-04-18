using RecipeApp.Services;
using RecipeApp.Models;
namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullUsername_Login_ThrowsException() {
        UserService us = new UserService();
        string username = null;
        string password = "RidaPassword";

        us.Login(username,password);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Login_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida1";
        string password = null;

        us.Login(username,password);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullUsername_Register_ThrowsException() {
        UserService us = new UserService();
        string username = null;
        string password = "RidaNewPassword";
        string description = "Hey im rida";

        us.Register(username, password, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Register_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida4";
        string password = null;
        string description = "Hey im rida";

        us.Register(username, password, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullDescription_Register_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida4";
        string password = "RidaNewPassword";
        string description = null;

        us.Register(username, password, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddToFavourites_NullRecipe_ThrowsArgumentException() {
        UserService userService = new();
        userService.AddToFavourites(null, new User("Test", "Test", "TestTest", new(), new()));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddToFavourites_NullUser_ThrowsArgumentException() {
        UserService userService = new();
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        userService.AddToFavourites(recipe, null);
    }
}