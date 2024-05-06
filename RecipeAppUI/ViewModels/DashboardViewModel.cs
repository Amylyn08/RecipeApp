namespace RecipeAppUI.ViewModels;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Services;
using RecipeApp.Models;
using RecipeApp.Searcher;
using System.Collections.Generic;
using System.Reactive;
using System;
using Avalonia.Controls;
using RecipeAppUI.Views;


public class DashboardViewModel : ViewModelBase {
    
    private string _dashboardErrorMessage = "";
    private RecipeService _recipeService = null;
    private List<Recipe> _recipes = new List<Recipe>();

    private string _selectedCriteria;
    private string _searchText;

    public string DashBoardErrorMessage {get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
    public RecipeService recipeService { get => _recipeService; set => _recipeService = value; }
    public List<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
    public ReactiveCommand<Unit, Unit> SearchCommand { get; }

    public string SelectedCriteria
    {
        get => _selectedCriteria;
        set => this.RaiseAndSetIfChanged(ref _selectedCriteria, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }


    public DashboardViewModel(SplankContext context) {
        recipeService = new(context);
        SearchCommand = ReactiveCommand.Create<Unit, Unit>((object sender) => SearchRecipes_SelectionChanged(sender, null));
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

    private void SearchRecipes_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        try
        {
            SearcherBase searcher;
            switch(_selectedCriteria){
                case "Keyword":
                    searcher = new SearchKeyWord(recipeService.Context, _searchText);
                    break;
                case "Username":
                    searcher = new SearchByUsername(recipeService.Context, _searchText);
                    break;
                case "Ingredients":
                    searcher = new SearchByIngredients(recipeService.Context, _searchText);
                    break;
                case "Price":
                    searcher = new SearchByPriceRange(recipeService.Context, Convert.ToDouble(_searchText));
                    break;
                case "Rating":
                    searcher = new SearchByRating(recipeService.Context, Int32.Parse(_searchText));
                    break;
                case "Servings":
                    searcher = new SearchByServings(recipeService.Context, Int32.Parse(_searchText));
                    break;
                case "Tags":
                    searcher = new SearchByTags(recipeService.Context, _searchText);
                    break;
                // case "Time":
                //     searcher = new SearchByTime(recipeService.Context, Int32.Parse(_searchText));
                //     break;
                default:
                    searcher = new SearchKeyWord(recipeService.Context, _searchText);
                    break;
                Recipes = searcher.FilterRecipes();
            }
        }
        catch (Exception e)
        {
            _dashboardErrorMessage = e.Message;
        }
    }   

}



