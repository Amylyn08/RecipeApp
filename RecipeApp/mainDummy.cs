using RecipeApp.Models;

namespace RecipeApp;

public class MainDummy {
    private static List<User> users = new() {
        new User("Rida1", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>()),
        new User("Rida2", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>()),
        new User("Rida3", "Real rida", "RidaPassword", new List<Recipe>(), new List<Recipe>())
    };

    private static User currentUser = null;

    public static void Main(string[] args) {
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

        Recipe recipe1 = new(users[0], "Potato recipe", 1, ingredients1, steps1, new(), new());
        Recipe recipe2 = new(users[1], "Potato recipe", 1, ingredients2, steps2, new(), new());
        Recipe recipe3 = new(users[2], "Potato recipe", 1, ingredients3, steps3, new(), new());

        users[0].MadeRecipes.Add(recipe1);
        users[1].MadeRecipes.Add(recipe2);
        users[2].MadeRecipes.Add(recipe3);

        Console.WriteLine("Enter 1 to login or 2 to register");
        int decision = GetDecision();

        Console.WriteLine("Enter username");
        string username = GetInput();

        Console.WriteLine("Enter password");
        string password = GetInput();

        string description = "Default";
        if (decision == 2) {
            foreach (User user in users) {
                if (user.Name == username) {
                    Console.WriteLine("Username already taken");
                    return;
                }
            }
            Console.WriteLine("Please enter a description of yourself");
            description = GetInput();
            users.Add(new(username, description, password, new(), new()));
        } 
        
        else {
            foreach (User user in users) {
                if (user.Name.Equals(username) && user.Password.Equals(password)) {
                    currentUser = user;
                    break;
                }
            }
            if (currentUser == null) {
                Console.WriteLine("Login failed !");
                return;
            } 
        }

        Console.Clear();
        Console.WriteLine("Here are your recipes !");
        foreach (Recipe recipe in currentUser.MadeRecipes) {
            Console.WriteLine(recipe);
        }
    }

    private static int GetDecision() {
        int decision = 0;
        do {
            try {
                decision = int.Parse(Console.ReadLine());
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
            if (decision != 1 && decision != 2) {
                Console.WriteLine("Please enter either 1 or 2");
            }
        } while (decision != 1 && decision != 2);
        return decision;
    }

    private static string GetInput() {
        string input = null;
        do {
            input = Console.ReadLine();
        } while (input == null);
        return input;
    }
}