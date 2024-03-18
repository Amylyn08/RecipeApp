using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class RecipeTest {
    [TestMethod]
    public void Constructor_Init() {
        int servings = 5;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>());
        List<Ingredient> ingredients = new() { 
            new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2), 
            new Ingredient("Salt", 5, UnitOfMeasurement.GRAMS, 2)
        };
        List<string> steps = new() {
            "Peel potato",
            "Put potato in boiling water",
            "Put salt in water",
            "Eat"
        };
        List<Rating> ratings = new();
        List<Tag> tags = new() {
            new Tag("Potato"),
            new Tag("Salt")
        };
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual(user, recipe.User);
        Assert.AreEqual(5, recipe.Servings);
        Assert.AreEqual("A salty potato", recipe.Description);
        Assert.AreEqual(2, recipe.Ingredients.Count);
        Assert.AreEqual(4, recipe.Steps.Count);
        Assert.AreEqual(0, recipe.Ratings.Count);
        Assert.AreEqual(2, recipe.Tags.Count);
    } 

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserNull_Throws_ArgumentException() {
        int servings = 3;
        string description = "Description";
        User user = null;
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    public void DescriptionNull_IsEmpty() {
        int servings = 3;
        string description = null;
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual("", recipe.Description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Description_Above_Limit_Throws_ArgumentException() {
        int servings = 3;
        string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Servings_0_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Null_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = null;
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Empty_Throws_ArgumentException() {
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Null_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = null;
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Empty_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        tags.Add(new Tag("Vegan"));
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ratings_Null_Throws_ArgumentException() {
        int servings = 3;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = null;
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        tags.Add(new Tag("Vegan"));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Null_Tags_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = null;
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        steps.Add("Do potato");
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tags_Above_3_Throws_ArgumentException() {
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>());
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new();
        List<Tag> tags = new();
        List<string> steps = new();
        ingredients.Add(new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100));
        ratings.Add(new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>())));
        steps.Add("Do potato");
        tags.Add(new Tag("Vegan"));
        tags.Add(new Tag("Healthy"));
        tags.Add(new Tag("Potate"));
        tags.Add(new Tag("Meat"));
        Recipe recipe = new(user, description, servings, ingredients, steps, ratings, tags);
    }  
}