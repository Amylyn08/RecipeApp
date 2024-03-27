namespace RecipeApp;

using RecipeApp.Models;

public class MockDatabase {
    public static List<User> Users { get; private set; }
    public static List<Recipe> AllRecipes { get; private set; }

    public MockDatabase() {
        Users = new() {
            new User("Rida1", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>()),
            new User("Rida2", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>()),
            new User("Rida3", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>())
        };

        Ingredient ingredient1 = new("Normal Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient2 = new("Sweet Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient3 = new("Sour Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient4 = new("Spicy Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient5 = new("Creamy Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient6 = new("Meat Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient7 = new("Peeled Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient8 = new("Awesome Potato", 1, UnitOfMeasurement.AMOUNT, 10);
        Ingredient ingredient9 = new("Boring Potato", 1, UnitOfMeasurement.AMOUNT, 10);

        Step step1 = new(10, "Do potato stuff");
        Step step2 = new(10, "Do more potato stuff");
        Step step3 = new(10, "Do lot's of potato stuff");
        Step step4 = new(10, "Do potatoey potato stuff");
        Step step5 = new(10, "Do I like potato stuff");
        Step step6 = new(10, "POTATOOOOO");
        Step step7 = new(10, "Insert potato step here");
        Step step8 = new(10, "Cut potatoes and put in water");
        Step step9 = new(10, "Become one with the potato");

        List<Ingredient> ingredients1 = new() { ingredient1, ingredient2, ingredient3 };
        List<Ingredient> ingredients2 = new() { ingredient4, ingredient5, ingredient6 };
        List<Ingredient> ingredients3 = new() { ingredient7, ingredient8, ingredient9 };

        List<Step> steps1 = new() { step1, step2, step3 };
        List<Step> steps2 = new() { step4, step5, step6 };
        List<Step> steps3 = new() { step7, step8, step9 };

        Recipe recipe1 = new("Easy Recipe", Users[0], "Potato recipe", 1, ingredients1, steps1, new(), new());
        Recipe recipe2 = new("Fast Recipe", Users[1], "Potato recipe", 1, ingredients2, steps2, new(), new());
        Recipe recipe3 = new("Cool Recipe", Users[2], "Potato recipe", 1, ingredients3, steps3, new(), new());

        AllRecipes.Add(recipe1);
        AllRecipes.Add(recipe2);
        AllRecipes.Add(recipe3);

        Users[0].MadeRecipes.Add(recipe1);
        Users[1].MadeRecipes.Add(recipe2);
        Users[2].MadeRecipes.Add(recipe3);
    }
}