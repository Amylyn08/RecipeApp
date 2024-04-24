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
    public static SearcherBase searcher;
    public static User currentUser = null;
    
    public static void Main() {
        AskLoginOrRegister();
    }

    public static void AskLoginOrRegister() {
        const int LOGIN = 1;
        const int REGISTER = 2;
        int input = 0;
        while (true) {
            try {
                Console.WriteLine($"Enter {LOGIN} to login");
                Console.WriteLine($"Enter {REGISTER} to register");
                input = int.Parse(Console.ReadLine());
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
        string username = "";
        string password = "";
        while (true) {
            try {
                Console.WriteLine("Enter username: ");
                username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
                currentUser = userService.Login(username, password);
                Console.WriteLine("Welcome ! You are now logged in !");
                break;
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
            } catch (UserDoesNotExistException e) {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void RegisterUser() {
        string username = "";
        string password = "";
        string description = "";
        while (true) {
            try {
                Console.WriteLine("Enter username: ");
                username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
                Console.WriteLine("Enter description: ");
                description = Console.ReadLine();
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

    private static void CreateRecipe() {
        while (true) {
            try {
                Console.WriteLine("Enter the name of your recipe: ");
                string name = GetInput();
                Console.WriteLine("Enter the description of your recipe");
                string description = GetInput();
                Console.WriteLine("Enter the amount of serving your recipe has");
                int servings = GetIntInput();
                Console.WriteLine("Create your ingredients:");
                List<Ingredient> ingredients = CreateListIngredients();
                Console.WriteLine("Add your steps:");
                List<Step> steps = CreateListStep();
                Console.WriteLine("Add your tags: ");
                List<Tag> tags = CreateListTags();
                Recipe recipe = new(name, _currentUser, description, servings, ingredients, steps, new(), tags);
                _recipeService.CreateRecipe(recipe);
                break;
            } catch (ArgumentException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
    private static string GetInput() {
        string input = null;
        do {
            input = Console.ReadLine();
        } while (input == null);
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
    
    return filteredRecipes;
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

    private static string GetInput() {
        string? input;
        do {
            input = Console.ReadLine();
        } while (input == null);
        return input;
    }

    // // private static List<Recipe> SearchRecipe() {
    // //     SearcherBase search = null;
    // //     Console.WriteLine("Enter 1 to Search By Keyword");
    // //     Console.WriteLine("Enter 2 to Search By Ingredient name");
    // //     Console.WriteLine("Enter 3 to Search By Price range");
    // //     Console.WriteLine("Enter 4 to Search By Rating");
    // //     Console.WriteLine("Enter 5 to Search By Serving");
    // //     Console.WriteLine("Enter 6 to Search By Tag name");
    // //     Console.WriteLine("Enter 7 to Search By Prepare Time range");
    // //     Console.WriteLine("Enter 8 to Search By Username");
    // //     int input = GetIntInput();
    // //     if(input == 1){
    // //         Console.WriteLine("Enter a keyword: ");
    // //         string keyword = GetInput();
    // //         search = new SearchKeyWord(keyword);
    // //     } else if(input == 2) {
    // //         Console.WriteLine("Enter an ingredient:");
    // //         string ingredientName = GetInput();
    // //         search = new SearchByIngredients(ingredientName);
    // //     } else if(input == 3) {
    // //         Console.WriteLine("Enter the min");
    // //         double min = double.Parse(Console.ReadLine());
    // //         Console.WriteLine("Enter the max");
    // //         double max = double.Parse(Console.ReadLine());
    // //         search = new SearchByPriceRange(min, max);
    // //     } else if(input == 4) {
    // //         Console.WriteLine("Enter rating to search");
    // //         int rating = GetIntInput();
    // //         search = new SearchByRating(rating);
    // //     } else if(input == 5) {
    // //         Console.WriteLine("Enter num of servings");
    // //         int serving = GetIntInput();
    // //         search = new SearchByServings(serving);
    // //     } else if(input == 6) {
    // //         Console.WriteLine("Enter tag name");
    // //         string tagName = GetInput();
    // //         search = new SearchByTags(tagName);
    // //     } else if(input == 7) {
    // //         Console.WriteLine("Enter min time");
    // //         int min = GetIntInput();
    // //         Console.WriteLine("Enter max time");
    // //         int max = GetIntInput();
    // //         search = new SearchByTime(min, max);
    // //     } else if(input == 8) {
    // //         Console.WriteLine("Enter username:");
    // //         string username = GetInput();
    // //         search = new SearchByUsername(username);
    // //     } else {
    // //         Console.WriteLine("Invalid input");
    // //         return null;
    // //     }
    // //     return _recipeService.SearchRecipes(search);
    // // }

}
    // public static void Main(string[] args) {
    //     MockDatabase.Init();
    //     Console.WriteLine("Enter 1 to login or 2 to register");
    //     int decision = GetDecision();
    //     if (decision == 1) {
    //         int tries = 0;
    //         do {
    //             try {
    //                 Console.WriteLine("Enter username");
    //                 string username = GetInput();
    //                 Console.WriteLine("Enter password");
    //                 string password = GetInput();
    //                 _currentUser = _userService.Login(username, password);
    //                 if (_currentUser == null) {
    //                     Console.WriteLine("Login failed !");
    //                     tries++;
    //                     if (tries == 3) {
    //                         Console.WriteLine("You are locked out");
    //                         return;
    //                     }
    //                 } else {
    //                     break;
    //                 }
                    
    //             } catch (ArgumentException e) {
    //                 Console.WriteLine(e.Message);
    //             }
    //         } while (_currentUser == null);
    //     } else if (decision == 2) {
    //         do {
    //             try {
    //                 Console.WriteLine("Enter username");
    //                 string username = GetInput();
    //                 Console.WriteLine("Enter password");
    //                 string password = GetInput();
    //                 Console.WriteLine("Enter description");
    //                 string description = GetInput();
    //                 _currentUser = _userService.Register(username, password, description);
    //                 if (_currentUser is null) {
    //                     throw new ArgumentException("Username already taken");
    //                 }
    //                 else {
    //                     break;
    //                 }
    //             } catch (ArgumentException e) {
    //                 Console.WriteLine(e.Message);
    //             }
    //         } while (_currentUser == null);
    //     }

    //     Console.Clear();
    //     int input = 0;
    //     while (_currentUser != null) {
    //         Console.WriteLine("Here are your options");
    //         Console.WriteLine("Press 1 to view all your recipes");
    //         Console.WriteLine("Press 2 to create a recipe");
    //         Console.WriteLine("Press 3 to update a recipe");
    //         Console.WriteLine("Press 4 to search for recipes");
    //         Console.WriteLine("Press 5 to delete a recipe");
    //         Console.WriteLine("Press 6 to change your password");
    //         Console.WriteLine("Press 7 to delete your account");
    //         Console.WriteLine("Press 8 to view your favourited recipes");
    //         Console.WriteLine("Press 9 to update your favourite recipes");
    //         Console.WriteLine("Press 10 to try out hashing feature.");
    //         try {
    //             input = GetIntInput();
    //             if (input == 1) {
    //                 Console.Clear();
    //                 foreach (Recipe recipe in _currentUser.MadeRecipes) {
    //                     Console.WriteLine(recipe);
    //                     try {
    //                         Console.WriteLine("Loading nutrition facts...");
    //                         var nutrition = _nutritionFactFetcher.Fetch(recipe);
    //                         Console.WriteLine(nutrition);
    //                     } catch (Exception) {
    //                         Console.WriteLine("Could not fetch nutrition facts for this recipe");
    //                     }
    //                 }
    //             } else if (input == 2) {
    //                 Console.Clear();
    //                 CreateRecipe();
    //             } else if (input == 3) {
    //                 Console.Clear();
    //                 UpdateRecipe();
    //             // } else if (input == 4) {
    //             //     Console.Clear();
    //             //     List<Recipe> foundRecipes = SearchRecipe();
    //             //     Console.Clear();
    //             //     Console.WriteLine("FOUND RECIPES");
    //             //     foreach(Recipe recipe in foundRecipes) {
    //             //         Console.WriteLine(recipe);
    //             //         try {
    //             //             Console.WriteLine("Loading nutrition facts...");
    //             //             var nutrition = _nutritionFactFetcher.Fetch(recipe);
    //             //             Console.WriteLine(nutrition);
    //             //         } catch (Exception) {
    //             //             Console.WriteLine("Could not fetch nutrition facts for this recipe");
    //             //         }
    //             //     }
    //                 // FavouriteRecipes(foundRecipes);
    //             } else if (input == 5) {
    //                 Console.Clear();
    //                 DeleteRecipe();
    //             } else if (input == 6) {
    //                 Console.Clear();
    //                 ChangePassword();
    //             } else if (input == 7) {
    //                 Console.Clear();
    //                 DeleteAccount();
    //                 return;
    //             } else if (input == 8) {
    //                 Console.Clear();
    //                 Console.WriteLine("FAVOURITE RECIPES");
    //                 foreach (Recipe recipe in _currentUser.Favorites) {
    //                     Console.WriteLine(recipe);
    //                     try {
    //                         Console.WriteLine("Loading nutrition facts...");
    //                         var nutrition = _nutritionFactFetcher.Fetch(recipe);
    //                         Console.WriteLine(nutrition);
    //                     } catch (Exception) {
    //                         Console.WriteLine("Could not fetch nutrition facts for this recipe");
    //                     }
    //                 }
    //             } else if (input == 9) {
    //                 Console.Clear();
    //                 DeleteFromFavourites();
    //             }
    //                 else if (input == 10) {
    //                 Console.Clear();
    //                 HashInputPassword();
    //             }
    //         } catch (FormatException) {
    //             Console.WriteLine("Please enter a valid number");
    //         }
    //     }
    // }

    // private static void DeleteFromFavourites() {
    //     for (int i = 0; i < _currentUser.Favorites.Count; i++) {
    //         System.Console.WriteLine((i+1) + ": " + _currentUser.Favorites[i].Name);
    //     }
    //     System.Console.WriteLine("Enter recipe number to delete");
    //     int recipeNum = GetIntInput();
    //     try {
    //         //_userService.DeleteFromFavourites(_currentUser.Favorites[recipeNum - 1], _currentUser);
    //     }   
    //     catch (ArgumentOutOfRangeException) {
    //         System.Console.WriteLine("Please enter a valid number");
    //     }
    // }

    // private static void FavouriteRecipes(List<Recipe> recipes) {
    //     Console.WriteLine("Press f to favourite a recipe, anything else to cancel");
    //     string input = GetInput();
    //     if (!input.Equals("f")) {
    //         return;
    //     }
    //     for (int i = 0; i < recipes.Count; i++) {
    //         Console.WriteLine((i + 1) + ": " + recipes[i].Name);
    //     }
    //     Console.WriteLine("Choose a recipe number to favourite");
    //     int num = GetIntInput();
    //     try {
    //         _userService.AddToFavourites(recipes[num - 1], _currentUser);
    //     } catch (ArgumentOutOfRangeException) {
    //         Console.WriteLine("You did not enter a valid number");
    //     } catch (ArgumentException e) {
    //         Console.WriteLine(e.Message);
    //     }
    // }

    // private static void DeleteRecipe() {
    //     for (int i = 0; i < _currentUser.MadeRecipes.Count; i++) {
    //         int recipeNum = i + 1;
    //         Console.WriteLine(recipeNum + ": " + _currentUser.MadeRecipes[i].Name);
    //     }
    //     while (true) {
    //         Console.WriteLine("Please choose recipe number to delete");
    //         try {
    //             _recipeService.DeleteRecipe(_currentUser.MadeRecipes[GetIntInput() - 1], _currentUser);
    //             break;
    //         } catch (ArgumentOutOfRangeException) {
    //             Console.WriteLine("Please enter a valid number");
    //         } catch (FormatException) {
    //             Console.WriteLine("Please enter a valid number");
    //         } catch (ArgumentException e) {
    //             Console.WriteLine(e.Message);
    //         }
    //     }
    // }

    // private static void DeleteAccount() {
    //     Console.WriteLine("Enter username");
    //     string username = GetInput();
    //     Console.WriteLine("Enter password");
    //     string password = GetInput();
    //     _currentUser = _userService.Login(username, password);
    //     if (_currentUser != null) {
    //         _userService.DeleteAccount(_currentUser);
    //         Console.WriteLine("Successfully deleted account");
    //         _currentUser = null;
    //     }
    //     else {
    //         Console.WriteLine("Failed to authenticate and delete account");
    //     }
    // }

    // private static void ChangePassword() {
    //     Console.WriteLine("Enter username");
    //     string username = GetInput();
    //     Console.WriteLine("Enter current password");
    //     string password = GetInput();
    //     _currentUser = _userService.Login(username, password);
    //     if (_currentUser == null) {
    //         Console.WriteLine("Login failed");
    //         return;
    //     }
    //     Console.WriteLine("Enter new password");
    //     string newPassword = GetInput();
    //     _userService.ChangePassword(_currentUser, newPassword);
    //     Console.WriteLine("Your password has been changed");
    //     Console.WriteLine("Please login once more...");
    //     Console.WriteLine("Enter username");
    //     username = GetInput();
    //     Console.WriteLine("Enter password");
    //     password = GetInput();
    //     _currentUser = _userService.Login(username, password);
    //     if (_currentUser is null) {
    //         System.Console.WriteLine("Login failed, you have been logged out");
    //         return;
    //     } else {
    //         System.Console.WriteLine("Welcome back !");
    //     }
    // }   

    // private static int GetDecision() {
    //     int decision = 0;
    //     do {
    //         try {
    //             decision = GetIntInput();
    //         } catch (FormatException) {
    //             Console.WriteLine("Please enter a valid number");
    //         }
    //         if (decision != 1 && decision != 2) {
    //             Console.WriteLine("Please enter either 1 or 2");
    //         }
    //     } while (decision != 1 && decision != 2);
    //     return decision;
    // }

    // /// <summary>
    // /// Allows the user to create a recipe
    // /// </summary>
    // /// <param name="user">The current user</param>
    // /// <returns>The Recipe object that the user made</returns>
    // private static void CreateRecipe() {
    //     while (true) {
    //         try {
    //             Console.WriteLine("Enter the name of your recipe: ");
    //             string name = GetInput();
    //             Console.WriteLine("Enter the description of your recipe");
    //             string description = GetInput();
    //             Console.WriteLine("Enter the amount of serving your recipe has");
    //             int servings = GetIntInput();
    //             Console.WriteLine("Create your ingredients:");
    //             List<Ingredient> ingredients = CreateListIngredients();
    //             Console.WriteLine("Add your steps:");
    //             List<Step> steps = CreateListStep();
    //             Console.WriteLine("Add your tags: ");
    //             List<Tag> tags = CreateListTags();
    //             Recipe recipe = new(name, _currentUser, description, servings, ingredients, steps, new(), tags);
    //             _recipeService.CreateRecipe(recipe, _currentUser);
    //             break;
    //         } catch (ArgumentException e) {
    //             Console.Clear();
    //             Console.WriteLine(e.Message);
    //         }
    //     }
    // }


    // /// <summary>
    // /// Asks the user to create an ingredient object
    // /// </summary>
    // /// <returns>An ingredient object</returns>
    private static Ingredient CreateIngredient() {
        Console.WriteLine("Enter ingredient name:");
        string name = GetInput();

        Console.WriteLine("Enter unit of measurement: ");
        Console.WriteLine("1 (Spoons)");
        Console.WriteLine("2 (Grams)");
        Console.WriteLine("3 (Cups)");
        Console.WriteLine("4 (Teaspoons)");
        Console.WriteLine("5 (Amount)");
        
        int unit = 0;
        do { unit = GetIntInput(); } 
        while (unit != 1 && unit != 2 && unit != 3 && unit != 4 && unit != 5);
        
        Console.WriteLine("Enter the amount:");
        int quantity = GetIntInput();

        Console.WriteLine("Enter the price:");
        double price = GetIntInput();

        UnitOfMeasurement unitOfMeasurement = UnitOfMeasurement.AMOUNT;
        if (unit == 1) unitOfMeasurement = UnitOfMeasurement.SPOONS;
        else if (unit == 2) unitOfMeasurement = UnitOfMeasurement.GRAMS;
        else if (unit == 3) unitOfMeasurement = UnitOfMeasurement.CUPS;
        else if (unit == 4) unitOfMeasurement = UnitOfMeasurement.TEASPOONS;
        else if (unit == 5) unitOfMeasurement = UnitOfMeasurement.AMOUNT;
        return new Ingredient(name, quantity, unitOfMeasurement, price);
    }

    // /// <summary>
    // /// Creates a list of ingredients chosen by the user
    // /// </summary>
    // /// <returns>A list of ingredients</returns>
    private static List<Ingredient> CreateListIngredients() {
        List<Ingredient> ingredients = new List<Ingredient>();

        bool createIng = true;

        while(createIng) {
            Ingredient ingredient = CreateIngredient();
            ingredients.Add(ingredient);

            Console.WriteLine("Add another ingredient? (Y/N):");
            string choice = GetInput();
            if(choice.ToUpper() == "N") {
                createIng = false;
            }
        }
        return ingredients;
    }

    // /// <summary>
    // /// Asks the user to create a step
    // /// </summary>
    // /// <returns>The user made step</returns>
    // private static Step CreateSteps() {
    //     Console.WriteLine("Enter instruction details:");
    //     string instruction = GetInput();
    //     Console.WriteLine("Enter the amount of time in minutes:");
    //     int time = GetIntInput();
    //     return new Step(time, instruction);
    // }

    // /// <summary>
    // /// Using the CreateSteps() method, the user creates a list of steps
    // /// </summary>
    // /// <returns>A list of custom made steps</returns>
    // private static List<Step> CreateListStep() {
    //     List<Step> steps = new List<Step>();

    //     bool createStep = true;

    //     while(createStep) {
    //         Step step = CreateSteps();
    //         steps.Add(step);

    //         Console.WriteLine("Add Step? (Y/N):");
    //         string choice = GetInput();
    //         if(choice.ToUpper() == "N") {
    //             createStep = false;
    //         }
    //     }
    //     return steps;
    // }

    // public static List<Tag> CreateListTags() {
    //     List<Tag> tags = new();
    //     for (int i = 0; i < Constants.MAX_TAGS; i++) {
    //         Console.WriteLine("Please enter a tag, or nothing to exit");
    //         string input = GetInput();
    //         if (input.Length == 0) {
    //             return tags;
    //         }
    //         Tag tag = new(input);
    //         tags.Add(tag);
    //     }
    //     return tags;
    // }

    // private static void UpdateRecipe() {
    //     for (int i = 0; i < _currentUser.MadeRecipes.Count; i++) {
    //         int recipeNum = i + 1;
    //         Console.WriteLine(recipeNum + ": " + _currentUser.MadeRecipes[i].Name);
    //     }
    //     while (true) {
    //         Console.WriteLine("Please choose recipe number to update");
    //         try {
    //             Recipe recipeToUpdate = _currentUser.MadeRecipes[GetIntInput() - 1];
    //             UpdateSingleRecipe(recipeToUpdate);
    //             break;
    //         } catch (ArgumentOutOfRangeException) {
    //             Console.WriteLine("Please enter a valid number");
    //         } catch (FormatException) {
    //             Console.WriteLine("Please enter a valid number");
    //         } catch (ArgumentException e) {
    //             Console.WriteLine(e.Message);
    //         }
    //     }
    // }

    // private static void UpdateSingleRecipe(Recipe recipeToUpdate) {
    //     Console.WriteLine("Enter 1 to change description");
    //     Console.WriteLine("Enter 2 to change servings");
    //     Console.WriteLine("Enter 3 to change ingredients");
    //     Console.WriteLine("Enter 4 to change steps");
    //     Console.WriteLine("Enter 5 to change tags");
    //     Console.WriteLine("Enter 6 to change name");
    //     int input = GetIntInput();
    //     if (input == 1) {
    //         Console.WriteLine("Enter new description");
    //         recipeToUpdate.Description = GetInput();
    //     } else if (input == 2) {
    //         Console.WriteLine("Enter new servings");
    //         recipeToUpdate.Servings = GetIntInput();            
    //     } else if (input == 3) {
    //         Console.WriteLine("Enter new ingredients");
    //         recipeToUpdate.Ingredients = CreateListIngredients();
    //     } else if (input == 4) {
    //         Console.WriteLine("Enter new steps");
    //         recipeToUpdate.Steps = CreateListStep();
    //     } else if (input == 5) {
    //         Console.WriteLine("Enter new tags");
    //         recipeToUpdate.Tags = CreateListTags();
    //     } else if (input == 6) {
    //         Console.WriteLine("Enter new name");
    //         recipeToUpdate.Name = GetInput();
    //     }
    //     _recipeService.UpdateRecipe(recipeToUpdate, _currentUser);
    // }

    // /// <summary>
    // /// Rates a recipe
    // /// </summary>
    // /// <param name="recipeToRate">Recipe thats getting rated</param>
    // private static void RatingRecipe(Recipe recipeToRate) {
    //     Rating newRating = CreateRating();
    //     _ratingService.RatingRecipe(recipeToRate, newRating);
    // }

    // /// <summary>
    // /// Creates a rating
    // /// </summary>
    // /// <returns>A Rating made by the user</returns>
    // private static Rating CreateRating() {
    //     Console.WriteLine("How many stars would you like to rate this recipe:");
    //     int stars = int.Parse(Console.ReadLine());
    //     Console.WriteLine("Write a review!");
    //     string description = GetInput();
    //     return _ratingService.CreateRating(_currentUser, stars, description);
    // }

    // /// <summary>
    // /// Filters the list of recipes to a certain criteria
    // /// </summary>
    // /// <returns>A filtered list of recipes</returns>
    // // private static List<Recipe> SearchRecipe() {
    // //     SearcherBase search = null;
    // //     Console.WriteLine("Enter 1 to Search By Keyword");
    // //     Console.WriteLine("Enter 2 to Search By Ingredient name");
    // //     Console.WriteLine("Enter 3 to Search By Price range");
    // //     Console.WriteLine("Enter 4 to Search By Rating");
    // //     Console.WriteLine("Enter 5 to Search By Serving");
    // //     Console.WriteLine("Enter 6 to Search By Tag name");
    // //     Console.WriteLine("Enter 7 to Search By Prepare Time range");
    // //     Console.WriteLine("Enter 8 to Search By Username");
    // //     int input = GetIntInput();
    // //     if(input == 1){
    // //         Console.WriteLine("Enter a keyword: ");
    // //         string keyword = GetInput();
    // //         search = new SearchKeyWord(keyword);
    // //     } else if(input == 2) {
    // //         Console.WriteLine("Enter an ingredient:");
    // //         string ingredientName = GetInput();
    // //         search = new SearchByIngredients(ingredientName);
    // //     } else if(input == 3) {
    // //         Console.WriteLine("Enter the min");
    // //         double min = double.Parse(Console.ReadLine());
    // //         Console.WriteLine("Enter the max");
    // //         double max = double.Parse(Console.ReadLine());
    // //         search = new SearchByPriceRange(min, max);
    // //     } else if(input == 4) {
    // //         Console.WriteLine("Enter rating to search");
    // //         int rating = GetIntInput();
    // //         search = new SearchByRating(rating);
    // //     } else if(input == 5) {
    // //         Console.WriteLine("Enter num of servings");
    // //         int serving = GetIntInput();
    // //         search = new SearchByServings(serving);
    // //     } else if(input == 6) {
    // //         Console.WriteLine("Enter tag name");
    // //         string tagName = GetInput();
    // //         search = new SearchByTags(tagName);
    // //     } else if(input == 7) {
    // //         Console.WriteLine("Enter min time");
    // //         int min = GetIntInput();
    // //         Console.WriteLine("Enter max time");
    // //         int max = GetIntInput();
    // //         search = new SearchByTime(min, max);
    // //     } else if(input == 8) {
    // //         Console.WriteLine("Enter username:");
    // //         string username = GetInput();
    // //         search = new SearchByUsername(username);
    // //     } else {
    // //         Console.WriteLine("Invalid input");
    // //         return null;
    // //     }
    // //     return _recipeService.SearchRecipes(search);
    // // }

    // /// <summary>
    // /// Prints the list of recipes
    // /// </summary>
    // /// <param name="recipes">The list of recipes thats gonna be printed</param>
    // private static void PrintRecipes(List<Recipe> recipes) {
    //     int count =1;
    //     foreach(Recipe recipe in recipes) {
    //         Console.WriteLine(count + ":");
    //         Console.WriteLine(recipe);
    //         count++;
    //     }
    // }

    // /// <summary>
    // /// Chooses a recipe from the list of recipes
    // /// </summary>
    // /// <param name="recipes">The list of recipe the user is choosing from</param>
    // /// <returns>The specific recipe the user chooses</returns>
    // private static Recipe ChooseRecipe(List<Recipe> recipes) {
    //     PrintRecipes(recipes);
    //     Console.WriteLine("Choose a recipe by entering its number: ");
    //     int choice = GetIntInput();
    //     while(choice > recipes.Count) {
    //         Console.WriteLine("Invalid choice. Please enter a valid number.");
    //         Console.WriteLine("Choose a recipe by entering its number: ");
    //         choice = GetIntInput();
    //     }
    //     return recipes[choice - 1];
        
    // }

    // private static void HashInputPassword(){
    //     Console.WriteLine("Please enter your desired password to view hash sample.");
    //     string passwordToHash = Console.ReadLine();
    //     PasswordEncrypter enc = new();
    //     Console.WriteLine(enc.Encrypt(passwordToHash));
    // }
