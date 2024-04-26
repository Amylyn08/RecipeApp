using RecipeApp.Context;
using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Api;
using RecipeApp.Searcher;
using RecipeApp.Models;
using RecipeApp.Exceptions;

namespace RecipeApp;

public class MainDummy {
    public static SplankContext splankContext = new();
    public static  PasswordEncrypter passwordEncrypter = new();
    public static IApiForIngredients apiForIngredients = new NutritionFactFetcher();
    public static UserService userService = new(splankContext, passwordEncrypter);
    public static RatingService ratingService = new(splankContext);
    public static RecipeService recipeService = new(splankContext);
    public static SearcherBase? searcher;
    public static User currentUser = null!;
    
    public static void Main() {
        AskLoginOrRegister();
        ShowOptions();
    }

    public static void ShowOptions() {
        Console.WriteLine($"Welcome {currentUser.Name} !");
        const int CHANGE_PASSWORD = 1;
        const int DELETE_ACCOUNT = 2;
        const int LOGOUT = 3;
        const int SEARCH = 4;
        const int VIEW_RECIPES = 5;
        const int CREATE_RECIPE = 6;
        while (currentUser is not null) {
            try {
                Console.WriteLine($"Enter {CHANGE_PASSWORD} to change password");
                Console.WriteLine($"Enter {DELETE_ACCOUNT} to delete account");
                Console.WriteLine($"Enter {LOGOUT} to logout");
                Console.WriteLine($"Enter {SEARCH} to search for recipes");
                Console.WriteLine($"Enter {VIEW_RECIPES} to view your recipes");
                Console.WriteLine($"Enter {CREATE_RECIPE} to create a new recipe");
                int input = int.Parse(Console.ReadLine()!);
                switch (input) {
                    case CHANGE_PASSWORD:
                        ChangePassword();
                        break;
                    case DELETE_ACCOUNT:
                        DeleteAccount();
                        break;
                    case LOGOUT:
                        Logout();
                        break;
                    case SEARCH:
                        List<Recipe> recipes = SearchRecipe();
                        if (recipes is null || recipes.Count == 0) {
                            throw new Exception();
                        }
                        PrintRecipes(recipes);
                        break;
                    case VIEW_RECIPES:
                        DisplayRecipes();
                        break;
                    case CREATE_RECIPE:
                        CreateRecipe();
                        break;
                    default:
                        throw new FormatException(); 
                }
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid option");
            }
        }
        Console.WriteLine("Goodbye !");
    }

    public static void AskLoginOrRegister() {
        const int LOGIN = 1;
        const int REGISTER = 2;
        while (true) {
            try {
                Console.WriteLine($"Enter {LOGIN} to login");
                Console.WriteLine($"Enter {REGISTER} to register");
                int input = int.Parse(Console.ReadLine()!);
                if (input == LOGIN) {
                    LoginUser();
                    break;
                } else if (input == REGISTER) {
                    RegisterUser();
                    break;
                } else {
                    throw new FormatException();
                }
            } catch (FormatException) {
                Console.WriteLine($"Please enter either {LOGIN} or {REGISTER}");
            }
        }
    }

    public static void LoginUser() {
        while (true) {
            try {
                Console.WriteLine("Enter username: ");
                string username = Console.ReadLine()!;
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine()!;
                currentUser = userService.Login(username, password);
                break;
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            } catch (UserDoesNotExistException e) {
                Console.WriteLine(e.Message);
            } catch (InvalidCredentialsException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void RegisterUser() {
        while (true) {
            try {
                Console.WriteLine("Enter username: ");
                string username = Console.ReadLine()!;
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine()!;
                Console.WriteLine("Enter description: ");
                string description = Console.ReadLine()!;
                userService.Register(username, password, description);
                Console.WriteLine("You have been registered !");
                LoginUser();
                break;
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            } catch (UserAlreadyExistsException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void ChangePassword() {
        while (true) {
            try {
                Console.WriteLine("Enter new password");
                string newPassword = Console.ReadLine()!;
                userService.ChangePassword(currentUser, newPassword);
                Console.WriteLine("Your password has been changed !");
                break;
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void DeleteAccount() {
        try {
            userService.DeleteAccount(currentUser);
        } catch (ArgumentException e) {
            Console.WriteLine(e.Message);
        }
        Logout();
    }

    public static void Logout() {
        Environment.Exit(0);
    }

    public static void DisplayRecipes() {
        if (currentUser.MadeRecipes is null || currentUser.MadeRecipes.Count == 0) {
            Console.WriteLine("No recipes to show !");
            return;
        }
        foreach (Recipe recipe in currentUser.MadeRecipes) {
            Console.WriteLine(recipe);
        }
    }

    private static void CreateRecipe() {
        while (true) {
            try {
                Console.WriteLine("Enter the name of your recipe: ");
                string name = Console.ReadLine()!;
                Console.WriteLine("Enter the description of your recipe");
                string description = Console.ReadLine()!;
                Console.WriteLine("Enter the amount of serving your recipe has");
                int servings = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Create your ingredients:");
                List<Ingredient> ingredients = CreateListIngredients();
                Console.WriteLine("Add your steps:");
                List<Step> steps = CreateListStep();
                Console.WriteLine("Add your tags: ");
                List<Tag> tags = CreateListTags();
                Recipe recipe = new(name, currentUser, description, servings, ingredients, steps, new(), tags);
                recipeService.CreateRecipe(recipe);
                break;
            } catch (ArgumentException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }

    private static Ingredient CreateIngredient() {
        Console.WriteLine("Enter ingredient name:");
        string name = Console.ReadLine()!;

        Console.WriteLine("Enter unit of measurement: ");
        Console.WriteLine("1 (Spoons)");
        Console.WriteLine("2 (Grams)");
        Console.WriteLine("3 (Cups)");
        Console.WriteLine("4 (Teaspoons)");
        Console.WriteLine("5 (Amount)");
        
        int unit = 0;
        do { unit = Convert.ToInt32(Console.ReadLine()); } 
        while (unit != 1 && unit != 2 && unit != 3 && unit != 4 && unit != 5);
        
        Console.WriteLine("Enter the amount:");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the price:");
        double price = Convert.ToInt32(Console.ReadLine());

        UnitOfMeasurement unitOfMeasurement = UnitOfMeasurement.AMOUNT;
        if (unit == 1) unitOfMeasurement = UnitOfMeasurement.SPOONS;
        else if (unit == 2) unitOfMeasurement = UnitOfMeasurement.GRAMS;
        else if (unit == 3) unitOfMeasurement = UnitOfMeasurement.CUPS;
        else if (unit == 4) unitOfMeasurement = UnitOfMeasurement.TEASPOONS;
        else if (unit == 5) unitOfMeasurement = UnitOfMeasurement.AMOUNT;
        return new Ingredient(name, quantity, unitOfMeasurement, price);
    }

        private static List<Ingredient> CreateListIngredients() {
        List<Ingredient> ingredients = new List<Ingredient>();

        bool createIng = true;

        while(createIng) {
            Ingredient ingredient = CreateIngredient();
            ingredients.Add(ingredient);

            Console.WriteLine("Add another ingredient? (Y/N):");
            string choice = Console.ReadLine()!;
            if(choice.ToUpper() == "N") {
                createIng = false;
            }
        }
        return ingredients;
    }

    private static List<Step> CreateListStep() {
        List<Step> steps = new List<Step>();

        bool createStep = true;

        while(createStep) {
            Step step = CreateSteps();
            steps.Add(step);

            Console.WriteLine("Add Step? (Y/N):");
            string choice = Console.ReadLine()!;
            if(choice.ToUpper() == "N") {
                createStep = false;
            }
        }
        return steps;
    }

    private static Step CreateSteps() {
        Console.WriteLine("Enter instruction details:");
        string instruction = Console.ReadLine()!;
        Console.WriteLine("Enter the amount of time in minutes:");
        int time = Convert.ToInt32(Console.ReadLine());
        return new Step(time, instruction);
    }

    public static List<Tag> CreateListTags() {
        List<Tag> tags = new();
        for (int i = 0; i < Constants.MAX_TAGS; i++) {
            Console.WriteLine("Please enter a tag, or nothing to exit");
            string input = Console.ReadLine()!;
            if (input.Length == 0) {
                return tags;
            }
            Tag tag = new(input);
            tags.Add(tag);
        }
        return tags;
    }
   private static void UpdateRecipe() {
        for (int i = 0; i < currentUser.MadeRecipes.Count; i++) {
            int recipeNum = i + 1;
            Console.WriteLine(recipeNum + ": " + currentUser.MadeRecipes[i].Name);
        }
        while (true) {
            Console.WriteLine("Please choose recipe number to update");
            try {
                Recipe recipeToUpdate = currentUser.MadeRecipes[Convert.ToInt32(Console.ReadLine()) - 1];
                UpdateSingleRecipe(recipeToUpdate);
                break;
            } catch (ArgumentOutOfRangeException) {
                Console.WriteLine("Please enter a valid number");
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    private static void UpdateSingleRecipe(Recipe recipeToUpdate) {
        Console.WriteLine("Enter 1 to change description");
        Console.WriteLine("Enter 2 to change servings");
        Console.WriteLine("Enter 3 to change ingredients");
        Console.WriteLine("Enter 4 to change steps");
        Console.WriteLine("Enter 5 to change tags");
        Console.WriteLine("Enter 6 to change name");
        Recipe updatedRecipe = recipeToUpdate;
        int input = Convert.ToInt32(Console.ReadLine());
        if (input == 1) {
            Console.WriteLine("Enter new description");
            updatedRecipe.Description = Console.ReadLine()!;
        } else if (input == 2) {
            Console.WriteLine("Enter new servings");
            updatedRecipe.Servings = Convert.ToInt32(Console.ReadLine());            
        } else if (input == 3) {
            Console.WriteLine("Enter new ingredients");
            updatedRecipe.Ingredients = CreateListIngredients();
        } else if (input == 4) {
            Console.WriteLine("Enter new steps");
            updatedRecipe.Steps = CreateListStep();
        } else if (input == 5) {
            Console.WriteLine("Enter new tags");
            updatedRecipe.Tags = CreateListTags();
        } else if (input == 6) {
            Console.WriteLine("Enter new name");
            updatedRecipe.Name = Console.ReadLine()!;
        }
        recipeService.UpdateRecipe(recipeToUpdate, updatedRecipe);
    }
    private static void DeleteRecipe() {
        for (int i = 0; i < currentUser.MadeRecipes.Count; i++) {
            int recipeNum = i + 1;
            Console.WriteLine(recipeNum + ": " + currentUser.MadeRecipes[i].Name);
        }
        while (true) {
            Console.WriteLine("Please choose recipe number to delete");
            try {
                recipeService.DeleteRecipe(currentUser.MadeRecipes[Convert.ToInt32(Console.ReadLine()) - 1]);
                break;
            } catch (ArgumentOutOfRangeException) {
                Console.WriteLine("Please enter a valid number");
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            }
        }
    }


    private static string GetInput() {
        string input = null!;
        do {
            input = Console.ReadLine()!;
        } while (input == null);
        return input;
    }

    /// <summary>
    /// Gets an integer input from a user
    /// </summary>
    /// <returns>The integer input</returns>
    private static int GetIntInput() {
        int input = 0;
        do {
            try {
                input = Convert.ToInt32(GetInput());
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
            if (input <= 0) {
                Console.WriteLine("Please enter an amount greater than 0");
            }
        } while (input <= 0);
        return input;
    }


    private static List<Recipe> SearchRecipe(){
    List<Recipe> filteredRecipes = new List<Recipe>();
    Console.WriteLine("Enter '1' to Search by Ingredient");
    Console.WriteLine("Enter '2' to Search by Keyword");
    Console.WriteLine("Enter '3' to Search by Price Range");
    Console.WriteLine("Enter '4' to Search by Rating");
    Console.WriteLine("Enter '5' to Search by Servings");
    Console.WriteLine("Enter '6' to Search by Tags");
    Console.WriteLine("Enter '7' to Search by Time");
    Console.WriteLine("Enter '8' to Search by a User's Favorite.");
    Console.WriteLine("Enter '9' to Search by Username");
    int choice = GetIntInput();
    switch (choice){
        case 1:
            Console.WriteLine("Enter the ingredient you would like to search by:");
            string ingredient = GetInput();
            searcher = new SearchByIngredients(splankContext, ingredient);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 2:
            Console.WriteLine("Enter the keyword you would like to search by");
            string keyword = GetInput();
            searcher = new SearchKeyWord(splankContext, keyword);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 3:
            Console.WriteLine("Enter the price range you would like to search for");
            Console.WriteLine("Min");
            int min = GetIntInput();
            Console.WriteLine("Max");
            int max = GetIntInput();
            searcher = new SearchByPriceRange(splankContext, min, max);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 4:
            Console.WriteLine("Enter the star rating you would like to search by");
            int rating = GetIntInput();
            searcher = new SearchByRating(splankContext, rating);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 5:
            Console.WriteLine("Enter the number of servings you would like to search by");
            int servings = GetIntInput();
            searcher = new SearchByServings(splankContext, servings);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 6:
            Console.WriteLine("Enter the name of tag you would like to search by");
            string tagName = GetInput();
            searcher = new SearchByTags(splankContext, tagName);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 7:
            Console.WriteLine("Enter the time range of recipe to search by");
            Console.Write("Min Time:");
            int minTime = GetIntInput();
            Console.Write("Max Time");
            int maxTime = GetIntInput();
            searcher = new SearchByTime(splankContext, minTime, maxTime);
            filteredRecipes = searcher.FilterRecipes();
        break;  
        case 8:
            Console.WriteLine("Viewing a user's favourite: Enter a username to search for..:");
            string nameUser = GetInput();
            SearchAllUsers nameSearcher = new (splankContext, nameUser);
            List<User> users = nameSearcher.GetUserByName();
            PrintUsers(users);
            Console.WriteLine("Select the number of user to view their favorites");
            int userIndex = GetIntInput();
            searcher = new SearchByUserFavorite(splankContext, users[userIndex]);
            filteredRecipes = searcher.FilterRecipes();
        break;
        case 9:
            Console.WriteLine("Enter a username to view all recipes made by them");
            string username = GetInput();
            searcher = new SearchByUsername(splankContext, username);
            filteredRecipes = searcher.FilterRecipes();
        break;
    }
    return filteredRecipes;
    }

    public static void PrintUsers(List<User> users){
        int index = 0;
        foreach (User u in users){
            Console.WriteLine($"[{index}]. Username: {u.Name}, Description: {u.Description}, Number of favorites: {u.Favorites.Count}");
            index ++;
        }
    } 

    public static void PrintRecipes(List<Recipe> recipes){
        int index = 0;
        foreach(Recipe r in recipes){
            Console.WriteLine($"[{index}]: {r}");
            index ++;
        }
    }
}

//hi