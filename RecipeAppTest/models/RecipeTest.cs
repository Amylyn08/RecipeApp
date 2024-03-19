using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class RecipeTest {
    [TestMethod]
    public void Constructor_Init() {
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual(user, recipe.User);
        Assert.AreEqual(1, recipe.Servings);
        Assert.AreEqual("A salty potato", recipe.Description);
        Assert.AreEqual(1, recipe.Ingredients.Count);
        Assert.AreEqual(1, recipe.Steps.Count);
        Assert.AreEqual(0, recipe.Ratings.Count);
        Assert.AreEqual(1, recipe.Tags.Count);
    } 

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserNull_Throws_ArgumentException() {
        int servings = 3;
        string description = "Description";
        User user = null;
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    public void DescriptionNull_IsEmpty() {
        int servings = 3;
        string description = null;
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual("", recipe.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Description_Above_Limit_Throws_ArgumentException() {
        int servings = 3;
        string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Servings_0_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Null_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = null;
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Empty_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Null_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new();
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Empty_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new();
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ratings_Null_Throws_ArgumentException() {
        int servings = 3;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = null;
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Null_Tags_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = null;
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tags_Above_3_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>())) };
        List<Tag> tags = new() { 
            new Tag("Vegan"),
            new Tag("Vegan"),
            new Tag("Vegan"),
            new Tag("Vegan") 
        };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    public void TimeToCook_Returns_15() {
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>());
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { 
            new Step(5, "Peel potato"), 
            new Step(5, "Boil potato"),
            new Step(5, "Season potato")
        };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual(15, recipe.GetTimeToCook());
    }  
}
