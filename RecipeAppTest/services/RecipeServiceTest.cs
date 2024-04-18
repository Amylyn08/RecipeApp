using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;

namespace RecipeAppTest.Services;

[TestClass]
public class RecipeServiceTest {
    
    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void CreateRecipe_NullRecipe_ThrowsException() {
    //     Recipe recipe = null;
    //     User user = new User("Amy", "Fanta stick person", "AmyPassword", new(), new());
    //     RecipeService rs = new RecipeService();

    //     rs.CreateRecipe(recipe, user);
    // }

    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void CreateRecipe_NullUser_ThrowsException() {
    //     string name = "Potato esquisite";
    //     int servings = 1;
    //     string description = "A salty potato";
    //     User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>());
    //     List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
    //     List<Step> steps = new() { new Step(5, "Boil potato") };
    //     List<Rating> ratings = new();
    //     List<Tag> tags = new() { new Tag("Potato"),};
    //     Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    //     RecipeService rs = new RecipeService();
    //     rs.CreateRecipe(recipe, null);
    // }

    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void DeleteRecipe_NullRecipe_ThrowsException() {
    //     Recipe recipe = null;
    //     User user = new User("Amy", "Fanta stick person", "AmyPassword", new(), new());
    //     RecipeService rs = new RecipeService();

    //     rs.DeleteRecipe(recipe, user);
    // }

    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void DeleteRecipe_NullUser_ThrowsException() {
    //     User user = null;
    //     Recipe recipe = new Recipe("Spaghetti", user, "Yummy food", 5, new(), new(), new(), new());
    //     RecipeService rs = new RecipeService();

    //     rs.DeleteRecipe(recipe, user);
    // }

    // // [TestMethod]
    // // [ExpectedException(typeof(ArgumentException))]
    // // public void SearchRecipe_NullSearcher_ThrowsException() {
    // //     ISearcher searcher = null;
    // //     RecipeService rs = new RecipeService();

    // //     rs.SearchRecipes(searcher);
    // // }

    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void UpdateRecipe_NullRecipe_ThrowsException() {
    //     Recipe recipe = null;
    //     User user = new User("Rida", "Unit testing hell", "RidaPassword", new(), new());
    //     RecipeService rs = new RecipeService();

    //     rs.UpdateRecipe(recipe, user);
        
    // }

    // [TestMethod]
    // [ExpectedException(typeof(ArgumentException))]
    // public void UpdateRecipe_NullUser_ThrowsException() {
    //     string name = "Potato esquisite";
    //     int servings = 1;
    //     string description = "A salty potato";
    //     User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>());
    //     List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
    //     List<Step> steps = new() { new Step(5, "Boil potato") };
    //     List<Rating> ratings = new();
    //     List<Tag> tags = new() { new Tag("Potato"),};
    //     Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    //     RecipeService rs = new RecipeService();
    //     rs.UpdateRecipe(recipe, null);
}