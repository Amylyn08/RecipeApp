using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using RecipeAppUI.Session;
using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Input;
namespace RecipeAppUI.ViewModels;

public class RecipesViewModel : ViewModelBase {
    private readonly RecipeService _recipeService;
    private List<Recipe> _recipes = new List<Recipe>();
    private MainWindowViewModel _mainWindowViewModel;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public List<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }


    public RecipesViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
    {
        _recipeService = new RecipeService(context);
        MainWindowViewModel = mainWindowViewModel;
        ChangeRecipe();
        
    }

    public void ChangeRecipe() {
        SearcherBase searcher = new SearchByUsername(_recipeService.Context, UserSingleton.GetInstance().Name);
        Recipes = _recipeService.SearchRecipes(searcher);
    }
}