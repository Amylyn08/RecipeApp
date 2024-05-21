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
using System.IO;
using System.Threading.Tasks;
using DynamicData;


namespace RecipeAppUI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _dashboardErrorMessage = "";
        private readonly RecipeService _recipeService;
        private ObservableCollection<Recipe> _recipes = [];
        private string _selectedCriteria = null!;
        private string _searchText = null!;
        private string _searchingMessage = null!;
        private string _selectedIndex = "0";
        private UserService _userService = null!;
        private string _errorMessage = null!;
        private readonly List<int> _excludedIds = [];
        public string CurrentUser { get; set; }
        public Bitmap? Bitmap { get; set; }
        public string DashboardErrorMessage { get => _dashboardErrorMessage; set => this.RaiseAndSetIfChanged(ref _dashboardErrorMessage, value); }
        public ObservableCollection<Recipe> Recipes { get => _recipes; set => this.RaiseAndSetIfChanged(ref _recipes, value); }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; } = null!;
        public ReactiveCommand<string, Unit> ChangeCriteria { get; } = null!;
        public ReactiveCommand<int, Unit> AddToFavouritesCommand {get;} = null!;
        public ReactiveCommand<int, Unit> SpecificViewCommand{get;} = null!;
        public ReactiveCommand<Unit, Unit> FetchNextFewRecipesCommmand { get; } = null!;
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; } = null!;

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

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public DashboardViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
        {
            _recipeService = new RecipeService(context);
            UserService = new UserService(context, new());
            SearchCommand = ReactiveCommand.Create(SearchRecipes);
            ChangeCriteria = ReactiveCommand.Create<string>(ExecuteChangCriteria);
            AddToFavouritesCommand = ReactiveCommand.Create<int>(AddToFavourites);
            FetchNextFewRecipesCommmand = ReactiveCommand.Create(LoadMoreRecipes);
            SpecificViewCommand = ReactiveCommand.Create<int>(SpecificView);
            LogoutCommand = ReactiveCommand.Create(Logout);
            MainWindowViewModel = mainWindowViewModel;
            if (UserSingleton.GetInstance().ProfilePicture != null)
            {
                using MemoryStream memoryStream = new(UserSingleton.GetInstance().ProfilePicture!);
                Bitmap = new Bitmap(memoryStream);
            }
            CurrentUser = UserSingleton.GetInstance().Name;
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

        private void AddToRecipesToNotLoadAgain(List<Recipe> recipes) 
        {
            foreach (Recipe recipe in recipes)
            {
                _excludedIds.Add(recipe.RecipeId);
            }
        }

        private void Logout() {
            UserSingleton.NullifyUser();
            MainWindowViewModel.ChangeToHomeView();
        }

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

        public void SpecificView(int recipeId){
            try{
                Recipe recipe = Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
                MainWindowViewModel.ChangeToSpecificView(recipe);
            }
            catch (Exception){
                ErrorMessage = "Error: Unable to go to specific view.";
            }
        }

        public double GetRatingAvgForSingleRecipe(int recipeId){
            Recipe recipe = Recipes.FirstOrDefault(r => r.RecipeId == recipeId)!;
            return recipe.GetTotalAverageRating();
        }
    }
}
