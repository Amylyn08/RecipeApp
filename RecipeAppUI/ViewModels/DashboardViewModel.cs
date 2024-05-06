using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;

namespace RecipeAppUI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _dashboardErrorMessage = "";
        private readonly RecipeService _recipeService;
        private List<Recipe> _recipes = new List<Recipe>();
        private string _selectedCriteria;
        private string _searchText;
        private string _searchingMessage;
        private string _selectedIndex = "0";

        public string DashboardErrorMessage { get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
        public List<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; }
        public ReactiveCommand<string, Unit> ChangeCriteria { get; }
        public ReactiveCommand<Unit, Unit> ClickHandler { get ; }
        

        public string SearchMessage{
            get => _searchingMessage;
            set => this.RaiseAndSetIfChanged(ref _searchingMessage, value);
        }
        public string SelectedCriteria
        {
            get => _selectedCriteria;
            set {
                if (_selectedCriteria !=value){
                    _selectedCriteria = value;
                    this.RaiseAndSetIfChanged(ref _searchingMessage, value);
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public string SelectedIndex{
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public DashboardViewModel(SplankContext context)
        {
            _recipeService = new RecipeService(context);
            SearchCommand = ReactiveCommand.Create(SearchRecipes);
            ChangeCriteria = ReactiveCommand.Create<string>(ExecuteChangCriteria);
            // ClickHandler = ReactiveCommand.Create<Unit,Unit>(ExecuteClickHandler);
            GetRecipes();
        }

        public void ExecuteChangCriteria(string criteria){
            SelectedCriteria = criteria;
            _searchingMessage = "You are now searching by: " + criteria;
        }

        public void ExecuteClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e){
            _searchingMessage = "You are now searching by: " + SelectedCriteria;
        }

        private void SearchRecipes()
        {
            try
            {
                SearcherBase searcher = null;
                switch (_selectedIndex)
                {
                    case "0":
                        searcher = new SearchKeyWord(_recipeService.Context, _searchText);
                        break;
                    case  "1":
                        searcher = new SearchByUsername(_recipeService.Context, _searchText);
                        break;
                    case "2":
                        searcher = new SearchByIngredients(_recipeService.Context, _searchText);
                        break;
                    case "3":
                        searcher = new SearchByPriceRange(_recipeService.Context, Convert.ToDouble(_searchText));
                        break;
                    case "4":
                        searcher = new SearchByRating(_recipeService.Context, Int32.Parse(_searchText));
                        break;
                    case "5":
                        searcher = new SearchByServings(_recipeService.Context, Int32.Parse(_searchText));
                        break;
                    case "6":
                        searcher = new SearchByTags(_recipeService.Context, _searchText);
                        break;
                    // case "Time":
                    //     searcher = new SearchByTime(_recipeService.Context, Int32.Parse(_searchText));
                    //     break;
                }
                Recipes = _recipeService.SearchRecipes(searcher);
            }
            catch (ArgumentException e)
            {
                DashboardErrorMessage = e.Message;
            }
        }

        private void GetRecipes()
        {
            try
            {
                Recipes = _recipeService.GetAllRecipes();
            }
            catch (ArgumentException e)
            {
                DashboardErrorMessage = e.Message;
            }
        }
    }
}
