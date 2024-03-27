using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using RecipeAppTest.searcher;

namespace RecipeApp;

public class MainDummy {
    private static User? currentUser = null;
    private static readonly UserService _userService = new();

    public static void Main(string[] args) {
        MockDatabase.Init();

        Console.WriteLine("Enter 1 to login or 2 to register");
        int decision = GetDecision();

        if (decision == 1) {
            int tries = 0;
            do {
                try {
                    Console.WriteLine("Enter username");
                    string username = GetInput();
                    
                    Console.WriteLine("Enter password");
                    string password = GetInput();
                    
                    currentUser = _userService.Login(username, password);
                    
                    if (currentUser == null) {
                        Console.WriteLine("Login failed !");
                        tries++;
                        if (tries == 3) {
                            Console.WriteLine("You are locked out");
                            return;
                        }
                    } else {
                        break;
                    }
                    
                } catch (ArgumentException e) {
                    Console.WriteLine(e.Message);
                }
            } while (currentUser == null);
        }

        else if (decision == 2) {
            do {
                try {
                    Console.WriteLine("Enter username");
                    string username = GetInput();

                    Console.WriteLine("Enter password");
                    string password = GetInput();
                    
                    Console.WriteLine("Enter description");
                    string description = GetInput();

                    currentUser = _userService.Register(username, password, description);

                    break;
                } catch (ArgumentException e) {
                    Console.WriteLine(e.Message);
                }
            } while (currentUser == null);
        }



        

        // Console.Clear();

        int input = 0;
        while (true) {
            Console.WriteLine("Press 1 to view all your recipes");
            Console.WriteLine("Press 2 to create a recipe");
            Console.WriteLine("Press 3 to update a recipe");
            Console.WriteLine("Press 4 to search for recipes");
            Console.WriteLine("Here are your options");
            try {
                input = int.Parse(Console.ReadLine());
                if (input == 1) {
                    foreach (Recipe recipe in currentUser.MadeRecipes) {
                        Console.WriteLine(recipe);
                    }
                } else if (input == 2) {
                    Recipe newRecipe = CreateRecipe();
                    currentUser.MadeRecipes.Add(newRecipe);
                } else if (input == 3) {
                    UpdateRecipe();
                } else if (input == 4) {
                    List<Recipe> foundRecipes = SearchRecipe();
                    Console.WriteLine("FOUND RECIPES");
                    foreach(Recipe recipe in foundRecipes) {
                        Console.WriteLine(recipe);
                    }
                }
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
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

    /// <summary>
    /// Allows the user to create a recipe
    /// </summary>
    /// <param name="user">The current user</param>
    /// <returns>The Recipe object that the user made</returns>
    private static Recipe CreateRecipe() {
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
        
        Recipe recipe = new(name, currentUser, description, servings, ingredients, steps, new List<Rating>(), new List<Tag>());

        return recipe;
    }
    /// <summary>
    /// Gets an integer input from a user
    /// </summary>
    /// <returns>The integer input</returns>
    private static int GetIntInput() {
        int input = 0;
        do {
            try {
                input = int.Parse(Console.ReadLine());
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
            if (input <= 0) {
                Console.WriteLine("Please enter an amount greater than 0");
            }
        } while (input <= 0);
        return input;
    }
    /// <summary>
    /// Asks the user to create an ingredient object
    /// </summary>
    /// <returns>An ingredient object</returns>
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
    /// <summary>
    /// Creates a list of ingredients chosen by the user
    /// </summary>
    /// <returns>A list of ingredients</returns>
    private static List<Ingredient> CreateListIngredients() {
        List<Ingredient> ingredients = new List<Ingredient>();

        bool createIng = true;

        while(createIng) {
            Ingredient ingredient = CreateIngredient();
            ingredients.Add(ingredient);

            Console.WriteLine("Add ingredient? (Y/N):");
            string choice = GetInput();
            if(choice.ToUpper() == "N") {
                createIng = false;
            }
        }
        return ingredients;
    }
    /// <summary>
    /// Asks the user to create a step
    /// </summary>
    /// <returns>The user made step</returns>
    private static Step CreateSteps() {
        Console.WriteLine("Enter instruction details:");
        string instruction = GetInput();
        Console.WriteLine("Enter the amount of time in minutes:");
        int time = GetIntInput();
        return new Step(time, instruction);
    }

    /// <summary>
    /// Using the CreateSteps() method, the user creates a list of steps
    /// </summary>
    /// <returns>A list of custom made steps</returns>
    private static List<Step> CreateListStep() {
        List<Step> steps = new List<Step>();

        bool createStep = true;

        while(createStep) {
            Step step = CreateSteps();
            steps.Add(step);

            Console.WriteLine("Add Step? (Y/N):");
            string choice = GetInput();
            if(choice.ToUpper() == "N") {
                createStep = false;
            }
        }
        return steps;
    }

    public static List<Tag> CreateListTags() {
        List<Tag> tags = new();
        for (int i = 0; i < Constants.MAX_TAGS; i++) {
            Console.WriteLine("Please enter a tag, or nothing to exit");
            string input = GetInput();
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
                Recipe recipeToUpdate = currentUser.MadeRecipes[GetIntInput() - 1];
                UpdateSingleRecipe(recipeToUpdate);
                break;
            } catch (ArgumentOutOfRangeException) {
                Console.WriteLine("Please enter a valid number");
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
        }
    }

    private static void UpdateSingleRecipe(Recipe recipeToUpdate) {
        System.Console.WriteLine("Enter 1 to change description");
        System.Console.WriteLine("Enter 2 to change servings");
        System.Console.WriteLine("Enter 3 to change ingredients");
        System.Console.WriteLine("Enter 4 to change steps");
        System.Console.WriteLine("Enter 5 to change tags");
        System.Console.WriteLine("Enter 6 to change name");
        System.Console.WriteLine("Enter anything else to cancel");
        int input = GetIntInput();
        if (input == 1) {
            Console.WriteLine("Enter new description");
            recipeToUpdate.Description = GetInput();
        } else if (input == 2) {
            Console.WriteLine("Enter new servings");
            recipeToUpdate.Servings = GetIntInput();            
        } else if (input == 3) {
            Console.WriteLine("Enter new ingredients");
            recipeToUpdate.Ingredients = CreateListIngredients();
        } else if (input == 4) {
            Console.WriteLine("Enter new steps");
            recipeToUpdate.Steps = CreateListStep();
        } else if (input == 5) {
            Console.WriteLine("Enter new tags");
            recipeToUpdate.Tags = CreateListTags();
        } else if (input == 6) {
            Console.WriteLine("Enter new name");
            recipeToUpdate.Name = GetInput();
        } else {
            return;
        }
    }

    /// <summary>
    /// Rates a recipe
    /// </summary>
    /// <param name="recipeToRate">Recipe thats getting rated</param>
    private static void RatingRecipe(Recipe recipeToRate) {
        Rating newRating = CreateRating();
        recipeToRate.Ratings.Add(newRating);
    }

    /// <summary>
    /// Creates a rating
    /// </summary>
    /// <returns>A Rating made by the user</returns>
    private static Rating CreateRating() {
        Console.WriteLine("How many stars would you like to rate this recipe:");
        int stars = int.Parse(Console.ReadLine());
        Console.WriteLine("Write a review!");
        string description = GetInput();
        return new Rating(stars, description, currentUser);
    }

    /// <summary>
    /// Filters the list of recipes to a certain criteria
    /// </summary>
    /// <returns>A filtered list of recipes</returns>
    private static List<Recipe> SearchRecipe() {
        ISearcher search = null;
        Console.WriteLine("Enter 1 to Search By Keyword");
        Console.WriteLine("Enter 2 to Search By Ingredient name");
        Console.WriteLine("Enter 3 to Search By Price range");
        Console.WriteLine("Enter 4 to Search By Rating");
        Console.WriteLine("Enter 5 to Search By Serving");
        Console.WriteLine("Enter 6 to Search By Tag name");
        Console.WriteLine("Enter 7 to Search By Prepare Time range");
        Console.WriteLine("Enter 8 to Search By Username");
        int input = GetIntInput();
        if(input == 1){
            Console.WriteLine("Enter a keyword: ");
            string keyword = GetInput();
            search = new SearchKeyWord(keyword);
        } else if(input == 2) {
            Console.WriteLine("Enter an ingredient:");
            string ingredientName = GetInput();
            search = new SearchByIngredients(ingredientName);
        } else if(input == 3) {
            Console.WriteLine("Enter the min");
            double min = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the max");
            double max = double.Parse(Console.ReadLine());
            search = new SearchByPriceRange(min, max);
        } else if(input == 4) {
            Console.WriteLine("Enter rating to search");
            int rating = GetIntInput();
            search = new SearchByRating(rating);
        } else if(input == 5) {
            Console.WriteLine("Enter num of servings");
            int serving = GetIntInput();
            search = new SearchByServings(serving);
        } else if(input == 6) {
            Console.WriteLine("Enter tag name");
            string tagName = GetInput();
            search = new SearchByTags(tagName);
        } else if(input == 7) {
            Console.WriteLine("Enter min time");
            int min = GetIntInput();
            Console.WriteLine("Enter max time");
            int max = GetIntInput();
            search = new SearchByTime(min, max);
        } else if(input == 8) {
            Console.WriteLine("Enter username:");
            string username = GetInput();
            search = new SearchByUsername(username);
        } else {
            Console.WriteLine("Invalid input");
            return null;
        }
        return null;
    }

    /// <summary>
    /// Prints the list of recipes
    /// </summary>
    /// <param name="recipes">The list of recipes thats gonna be printed</param>
    private static void PrintRecipes(List<Recipe> recipes) {
        int count =1;
        foreach(Recipe recipe in recipes) {
            Console.WriteLine(count + ":");
            Console.WriteLine(recipe);
            count++;
        }
    }

    /// <summary>
    /// Chooses a recipe from the list of recipes
    /// </summary>
    /// <param name="recipes">The list of recipe the user is choosing from</param>
    /// <returns>The specific recipe the user chooses</returns>
    private static Recipe ChooseRecipe(List<Recipe> recipes) {
        PrintRecipes(recipes);
        Console.WriteLine("Choose a recipe by entering its number: ");
        int choice = GetIntInput();
        while(choice > recipes.Count) {
            Console.WriteLine("Invalid choice. Please enter a valid number.");
            Console.WriteLine("Choose a recipe by entering its number: ");
            choice = GetIntInput();
        }
        return recipes[choice - 1];
        
    }
}