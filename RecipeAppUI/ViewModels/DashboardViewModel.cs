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
using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using DynamicData;
using RecipeAppUI.Utils;


namespace RecipeAppUI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _dashboardErrorMessage = "";
        private readonly RecipeService _recipeService;
        private ObservableCollection<Recipe> _recipes = [];
        private string _selectedCriteria = null!;
        private string _searchText = null!;
        private string _selectedIndex = "0";
        private UserService _userService = null!;
        private string _errorMessage = null!;
        private readonly List<int> _excludedIds = [];
        public string CurrentUser { get; set; }
        public Bitmap? Bitmap { get; set; }
        private string _searchingMessage;
        public string DashboardErrorMessage { get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
        public ObservableCollection<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; } = null!;
        public ReactiveCommand<string, Unit> ChangeCriteria { get; } = null!;
        public ReactiveCommand<int, Unit> AddToFavouritesCommand {get;} = null!;
        public ReactiveCommand<int, Unit> SpecificViewCommand{get;} = null!;
        public ReactiveCommand<Unit, Unit> FetchNextFewRecipesCommmand { get; } = null!;
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; } = null!;

        /// <summary>
        /// get setter for the user service
        /// </summary>
        public UserService UserService {
            get => _userService;
            set => _userService = value;
        }

        /// <summary>
        /// Getter setter for the Error Message
        /// </summary>
        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

        /// <summary>
        /// Getter setter for the SearchMessage
        /// </summary>
        public string SearchMessage{
            get => _searchingMessage;
            set => this.RaiseAndSetIfChanged(ref _searchingMessage, value);
        }

        /// <summary>
        /// Getter setter for the selected criteria
        /// </summary>
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

        /// <summary>
        /// Getter setter for the search text 
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        /// <summary>
        /// Getter and setter for the selected index of the searcher dropdown
        /// </summary>
        public string SelectedIndex{
            get => _selectedIndex;
            set {
                this.RaiseAndSetIfChanged(ref _selectedIndex, value);
                switch(_selectedIndex){
                    case "0":
                        SearchMessage = "Search by keyword...";
                        break;
                    case "1":
                        SearchMessage = "Search by username...";
                        break;
                    case "2":
                        SearchMessage="Search by ingredient...";
                        break;
                    case "3":
                        SearchMessage="Search by budget...";
                        break;
                    case "4":
                        SearchMessage = "Search by rating...";
                        break;
                    case "5":
                        SearchMessage="Search by servings...";
                        break;
                    case "6":
                        SearchMessage="Search by tag..";
                        break;
                    case "7":
                        SearchMessage="Search by time in minutes...";
                        break;
                    default:
                        SearchMessage = "Search";
                        break;
                }
            }
        }

        /// <summary>
        /// Getter and setter for the main window view model.
        /// </summary>
        public MainWindowViewModel MainWindowViewModel { get; set; }

        /// <summary>
        /// Constructor for the DashboardViewModel, sets the user and the list of recipes when 
        /// loading.
        /// </summary>
        /// <param name="context">The Splank Context of the application</param>
        /// <param name="mainWindowViewModel">The main window view model.</param>
        public DashboardViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
        {
            _recipeService = new RecipeService(context);
            UserService = new UserService(context, new());
            SearchCommand = ReactiveCommand.Create(SearchRecipes);
            AddToFavouritesCommand = ReactiveCommand.Create<int>(AddToFavourites);
            FetchNextFewRecipesCommmand = ReactiveCommand.Create(LoadMoreRecipes);
            SpecificViewCommand = ReactiveCommand.Create<int>(SpecificView);
            LogoutCommand = ReactiveCommand.Create(Logout);
            MainWindowViewModel = mainWindowViewModel;
            if (UserSingleton.GetInstance().ProfilePicture != null)
                Bitmap = BitMapper.DoBitmap(UserSingleton.GetInstance().ProfilePicture!);
            CurrentUser = UserSingleton.GetInstance().Name;
            GetRecipes();
        }
        
        /// <summary>
        /// Method to search through recipes with a certain searcher and sets
        /// the observable recipe list. Handels recipe loading and clears error message
        /// if successful.
        /// </summary>
        private void SearchRecipes()
        {
            try
            {
                SearcherBase searcher = null!;
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
                    case "7":
                        searcher = new SearchByTime(_recipeService.Context, Int32.Parse(_searchText));
                        break;
                }
                Recipes = new ObservableCollection<Recipe>(_recipeService.SearchRecipes(searcher));
                _excludedIds.Clear();
                AddToRecipesToNotLoadAgain([.. Recipes]);
                DashboardErrorMessage = "";
            }
            catch (ArgumentException e)
            {
                DashboardErrorMessage = e.Message;
            }
        }

        /// <summary>
        /// Method to get some recipes and also ensures they don't load again.
        /// </summary>
        private void GetRecipes()
        {
            try
            {
                const int NUM_DEFAULT_RECIPES_TO_GET = 3;
                Recipes = new ObservableCollection<Recipe>(_recipeService.GetSomeRecipes(NUM_DEFAULT_RECIPES_TO_GET, _excludedIds));
                AddToRecipesToNotLoadAgain([.. Recipes]); // Observable collection -> List Collection
            }
            catch (ArgumentException e)
            {
                DashboardErrorMessage = e.Message;
            }
        }

        /// <summary>
        /// Method to load more recipes, then adds them to a function to not be loaded again.
        /// </summary>
        private void LoadMoreRecipes() 
        {
            try {
                const int NUM_DEFAULT_NUM_TO_GET_MORE_RECIPES = 2;
                List<Recipe> moreRecipes = _recipeService.GetSomeRecipes(NUM_DEFAULT_NUM_TO_GET_MORE_RECIPES, _excludedIds);
                AddToRecipesToNotLoadAgain(moreRecipes);
                Recipes.AddRange(moreRecipes);
            } catch (ArgumentException e) {
                DashboardErrorMessage = e.Message;
            }
        }

        /// <summary>
        /// Method to add the recipe id of each recipe to the excluded id list.
        /// </summary>
        /// <param name="recipes">The list of recipes to be excluded.</param>
        private void AddToRecipesToNotLoadAgain(List<Recipe> recipes) 
        {
            foreach (Recipe recipe in recipes)
            {
                _excludedIds.Add(recipe.RecipeId);
            }
        }

        /// <summary>
        /// To logout a user, it nullifies the user instance when a 
        /// user logs out and sets them to the home view.
        /// </summary>
        private void Logout() {
            UserSingleton.NullifyUser();
            MainWindowViewModel.ChangeToHomeView();
        }

        /// <summary>
        /// Method to add a recipe to a user's favorites list using the recipe id.
        /// </summary>
        /// <param name="recipeId">The recipe id of the recipe to be favorited.</param>
        public void AddToFavourites(int recipeId) {
            try {
                Recipe recipe = this.Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
                UserService.AddToFavourites(recipe, UserSingleton.GetInstance());
            } catch (ArgumentException e) {
                ErrorMessage = e.Message;
            } catch (AlreadyFavouritedException e) {
                ErrorMessage = e.Message;
            }
        }

        /// <summary>
        /// Changes the main window view model to be the specific view of a certain recipe
        /// by using the recipe id of the recipe.
        /// </summary>
        /// <param name="recipeId">The recipe id of the recipe to be focused on.</param>
        public void SpecificView(int recipeId){
            try{
                Recipe recipe = Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
                MainWindowViewModel.ChangeToSpecificView(recipe);
            }
            catch (Exception){
                ErrorMessage = "Error: Unable to go to specific view.";
            }
        }
    }
}
