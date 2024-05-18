using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using RecipeAppUI.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Input;
namespace RecipeAppUI.ViewModels;

public class RecipesViewModel : ViewModelBase {
    private readonly RecipeService _recipeService;
    private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
    private MainWindowViewModel _mainWindowViewModel;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public ObservableCollection<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
    public ReactiveCommand<int, Unit> DeleteRecipe { get; } = null!;
     public ReactiveCommand<int, Unit> UpdateRecipe { get; } = null!;
    public string ErrorMessage { get; set; } = null!;


    public RecipesViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
    {
        _recipeService = new RecipeService(context);
        MainWindowViewModel = mainWindowViewModel;
        ChangeRecipe();
        DeleteRecipe = ReactiveCommand.Create<int>(DeleteRecipeCommand);
        UpdateRecipe = ReactiveCommand.Create<int>(UpdateRecipeCommand);
        
    }

    public void ChangeRecipe() {
        SearcherBase searcher = new SearchByUsername(_recipeService.Context, UserSingleton.GetInstance().Name);
        Recipes = new ObservableCollection<Recipe>(_recipeService.SearchRecipes(searcher));
    }

    public void DeleteRecipeCommand(int recipeId) {
            Recipe recipeToDelete = Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        try {
            _recipeService.DeleteRecipe(recipeToDelete);
            Recipes.Remove(recipeToDelete);
        } catch (ArgumentException e) {
            ErrorMessage = e.Message;
        } 
    }

    public void UpdateRecipeCommand(int recipeId) {
        Recipe recipeToUpdate = Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        MainWindowViewModel.ChangeToUpdateRecipeView(recipeToUpdate);
    }



}