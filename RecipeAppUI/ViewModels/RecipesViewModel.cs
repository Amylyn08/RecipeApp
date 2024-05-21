using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using RecipeAppUI.Session;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Linq;
using System.Reactive.Linq;
namespace RecipeAppUI.ViewModels;

public class RecipesViewModel : ViewModelBase {
    private readonly RecipeService _recipeService;
    private ObservableCollection<Recipe> _recipes = [];
    private MainWindowViewModel _mainWindowViewModel = null!;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public ObservableCollection<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
    public ReactiveCommand<int, Unit> DeleteRecipe { get; } = null!;
     public ReactiveCommand<int, Unit> UpdateRecipe { get; } = null!;
    public string ErrorMessage { get; set; } = null!;

    /// <summary>
    /// Constructor that creates a new instance of RecipesViewModel
    /// </summary>
    /// <param name="context">DB context</param>
    /// <param name="mainWindowViewModel">Instance of the MainWindowView</param>
    public RecipesViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
    {
        _recipeService = new RecipeService(context);
        MainWindowViewModel = mainWindowViewModel;
        ChangeRecipe();
        DeleteRecipe = ReactiveCommand.Create<int>(DeleteRecipeCommand);
        UpdateRecipe = ReactiveCommand.Create<int>(UpdateRecipeCommand);
        
    }

    /// <summary>
    /// Loads the user's recipe
    /// </summary>
    public void ChangeRecipe() {
        SearcherBase searcher = new SearchByUsername(_recipeService.Context, UserSingleton.GetInstance().Name);
        Recipes = new ObservableCollection<Recipe>(_recipeService.SearchRecipes(searcher));
    }

    /// <summary>
    /// Command the delete recipe from your recipe list
    /// </summary>
    /// <param name="recipeId">The id of the recipe</param>
    public void DeleteRecipeCommand(int recipeId) {
        Recipe recipeToDelete = Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
        try {
            _recipeService.DeleteRecipe(recipeToDelete);
            Recipes.Remove(recipeToDelete);
        } catch (ArgumentException e) {
            ErrorMessage = e.Message;
        } 
    }

    /// <summary>
    /// Command that brings you to the UpdateRecipeView to update your recipe
    /// </summary>
    /// <param name="recipeId">The id of the recipe</param>
    public void UpdateRecipeCommand(int recipeId) {
        Recipe recipeToUpdate = Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
        MainWindowViewModel.ChangeToUpdateRecipeView(recipeToUpdate);
    }
}