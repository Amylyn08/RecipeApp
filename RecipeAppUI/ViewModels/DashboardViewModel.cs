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

    private void SearchRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedValue = selectedItem.Content.ToString();
            string searchBarText = SearchBar.Text;
            SearcherBase searcher;
            switch(selectedValue){
                case "Keyword":
                    searcher = new SearchKeyWord(recipeService.Context, searchBarText);
                    break;
                case "Username":
                    searcher = new SearchByUsername(recipeService.Context, searchBarText);
                    break;
                case "Ingredients":
                    searcher = new SearchByIngredients(recipeService.Context, searchBarText);
                    break;
                case "Price":
                    searcher = new SearchByPrice(recipeService.Context, searchBarText);
                    break;
                case "Rating":
                    searcher = new SearchByRating(recipeService.Context, searchBarText);
                    break;
                case "Servings":
                    searcher = new SearchByServings(recipeService.Context, searchBarText);
                    break;
                case "Tags":
                    searcher = new SearchByTags(recipeService.Context, searchBarText);
                    break;
                case "Time":
                    searcher = new SearchByTime(recipeService.Context, searchBarText);
                    break;
                default:
                    searcher = new SearchKeyWord(recipeService.Context, searchBarText);
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



