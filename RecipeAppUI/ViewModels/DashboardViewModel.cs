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
        private UserService _userService;
        private string _errorMessage;

        public string DashboardErrorMessage { get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
        public List<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; }
        public ReactiveCommand<string, Unit> ChangeCriteria { get; }
        public ReactiveCommand<int, Unit> AddToFavouritesCommand {get;}
        public UserService UserService {
            get => _userService;
            set => _userService = value;
        }

        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

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

        private ViewModelBase _contentViewModel;

        public ViewModelBase ContentViewModel {
            get => _contentViewModel;
            private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }

        public DashboardViewModel(SplankContext context)
        {
            _recipeService = new RecipeService(context);
            UserService = new UserService(context, new());
            SearchCommand = ReactiveCommand.Create(SearchRecipes);
            ChangeCriteria = ReactiveCommand.Create<string>(ExecuteChangCriteria);
            AddToFavouritesCommand = ReactiveCommand.Create<int>(AddToFavourites);
            GetRecipes();
        }

        public DashboardViewModel() {}



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

        public void AddToFavourites(int recipeId) {
            try {
                Recipe recipe = this.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
                UserService.AddToFavourites(recipe, UserSingleton.GetInstance());
            } catch (ArgumentException e) {
                ErrorMessage = e.Message;
            } catch (AlreadyFavouritedException e) {
                ErrorMessage = e.Message;
            }
        }
    }
}
