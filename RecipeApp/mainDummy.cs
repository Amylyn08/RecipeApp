using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using RecipeApp.Models;

public class mainDummy{
    public static void Main(string[] args){

        List<Recipe> recipes = new List<Recipe>();
        User user1 = new User("bob", "blash", "123bosdfsdfsdfb", new List<Recipe>());
        string desc = "I am nothing..";
        int servings = 10;
        Ingredient ingredient1 = new Ingredient("banana", 1, UnitOfMeasurement.AMOUNT, 100);
        Ingredient ingredient2 = new Ingredient("chocolate", 1, UnitOfMeasurement.AMOUNT, 100);
        Ingredient ingredient3 = new Ingredient("raspberries", 1, UnitOfMeasurement.AMOUNT, 100);

        List<Ingredient> ingredientsIncludeBanana = new List<Ingredient>{ingredient1, ingredient2};
        List<Ingredient> ingredientsNoInclude = new List<Ingredient>{ingredient2, ingredient3};
        Step step = new (10, "blah");
        Rating rating = new(4, "nice", user1);
        List<Step> steps = new List<Step>();
        steps.Add(step);
        List<Rating> ratings = new List<Rating>();
        ratings.Add(rating);
        List<Tag> tags = new List<Tag>();
        Tag tag = new("vegan");
        recipes.Add(new Recipe(user1, desc, servings, ingredientsIncludeBanana, steps, ratings, tags));
        recipes.Add(new Recipe(user1, desc, servings, ingredientsNoInclude, steps, ratings, tags));
        recipes.Add(new Recipe(user1, desc, servings, ingredientsNoInclude, steps, ratings, tags));
        recipes.Add(new Recipe(user1, desc, servings, ingredientsIncludeBanana, steps, ratings, tags));
        recipes.Add(new Recipe(user1, desc, servings, ingredientsNoInclude, steps, ratings, tags));

        ISearcher searcher = new SearchByIngredients("Banana");
        List<Recipe> includingBanana = searcher.FilterRecipes(recipes);
        Console.WriteLine(includingBanana.Count);
        

    }
}