using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class RecipeTest {
    [TestMethod]
    public void Constructor_Init() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
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
        string name = "Potato esquisite";
        int servings = 3;
        string description = "Description";
        User user = null;
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss",new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DescriptionNull_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 3;
        string description = null;
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Description_Above_Limit_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 3;
        string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Servings_0_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = null;
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ingredients_Empty_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 0;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new();
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new();
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Steps_Empty_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new();
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Ratings_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 3;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = null;
        List<Tag> tags = new() { new Tag("Vegan") };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Null_Tags_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = null;
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tags_Above_3_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "Description";
        User user = new("Username", "Description", "Password", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100) };
        List<Rating> ratings = new() { new Rating(5, "Rating", new User("name", "mama","passsssssss", new List<Recipe>(), new List<Recipe>(), "salt")) };
        List<Tag> tags = new() { 
            new Tag("Vegan"),
            new Tag("Vegan"),
            new Tag("Vegan"),
            new Tag("Vegan") 
        };
        List<Step> steps = new() { new Step(5, "Do potato")};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    public void TimeToCook_Returns_15() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { 
            new Step(5, "Peel potato"), 
            new Step(5, "Boil potato"),
            new Step(5, "Season potato")
        };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual(15, recipe.GetTimeToCook());
    }  

    [TestMethod]
    public void TotalPrice_Returns_15() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { 
            new Ingredient("Potato", 5, UnitOfMeasurement.AMOUNT, 5),
            new Ingredient("Potato", 5, UnitOfMeasurement.AMOUNT, 5),
            new Ingredient("Potato", 5, UnitOfMeasurement.AMOUNT, 5)
        };
        List<Step> steps = new() { new Step(5, "Peel potatoes") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        Assert.AreEqual(15, recipe.GetTotalPrice());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameNull_Throws_ArgumentException() {
        string name = null;
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameEmpty_Throws_ArgumentException() {
        string name = "";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Name = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Empty_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DescriptionSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Description = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DescriptionSetter_Above_Limit_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ServingsSetter_0_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato"),};
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Servings = 0;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IngredientsSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Ingredients = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IngredientsSetter_Empty_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Ingredients = new();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StepsSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Steps = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StepsSetter_Empty_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Steps = new();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RatingsSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Ratings = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TagsSetter_Null_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Tags = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Tags_Above_Limit_Throws_ArgumentException() {
        string name = "Potato esquisite";
        int servings = 1;
        string description = "A salty potato";
        User user = new("PotatoLover32", "I love potatoes", "PotatoPotatoPotatp", new List<Recipe>(), new List<Recipe>(), "salt");
        List<Ingredient> ingredients = new() { new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 2) };
        List<Step> steps = new() { new Step(5, "Boil potato") };
        List<Rating> ratings = new();
        List<Tag> tags = new() { new Tag("Potato") };
        Recipe recipe = new(name, user, description, servings, ingredients, steps, ratings, tags);
        recipe.Tags = new() {
            new Tag("one"),
            new Tag("two"),
            new Tag("Three"),
            new Tag("Four")
        };
    }
}
