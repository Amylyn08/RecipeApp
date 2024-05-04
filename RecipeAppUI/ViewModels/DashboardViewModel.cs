namespace RecipeAppUI.ViewModels;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Services;
using RecipeApp.Models;
using System.Collections.Generic;
using System.Reactive;
using System;

public class DashboardViewModel : ViewModelBase {
    
    private string _dashboardErrorMessage = "";
    private RecipeService _recipeService = null;
    private List<Recipe> _recipes = new List<Recipe>();

    public string DashBoardErrorMessage {get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
    public RecipeService recipeService { get => _recipeService; set => _recipeService = value; }
    public List<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }

    public DashboardViewModel(SplankContext context) {
        recipeService = new(context);
        GetRecipes();
    }

    public void GetRecipes(){
        try{
            Recipes = recipeService.GetAllRecipes();
        }
        catch(ArgumentException e){
            _dashboardErrorMessage = e.Message;
        }
    }
}



