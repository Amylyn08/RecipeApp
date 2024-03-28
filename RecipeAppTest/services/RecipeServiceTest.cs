using RecipeApp.Models;
using RecipeApp.Services;

namespace RecipeAppTest.Services;

[TestClass]
public class RecipeServiceTest {
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateRecipe_NullRecipe_ThrowsException() {
        Recipe recipe = null;
        User user = new User("Amy", "Fanta stick person", "AmyPassword", new(), new());
        RecipeService rs = new RecipeService();

        rs.CreateRecipe(recipe, user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteRecipe_NullRecipe_ThrowsException() {
        Recipe recipe = null;
        User user = new User("Amy", "Fanta stick person", "AmyPassword", new(), new());
        RecipeService rs = new RecipeService();

        rs.DeleteRecipe(recipe, user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteRecipe_NullUser_ThrowsException() {
        User user = null;
        Recipe recipe = new Recipe("Spaghetti", user, "Yummy food", 5, new(), new(), new(), new());
        RecipeService rs = new RecipeService();

        rs.DeleteRecipe(recipe, user);
    }

    [TestMethod]
    [ExpectedException]
    public void



}