using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Searcher;
using System.Collections.Generic;
using ReactiveUI;
using RecipeAppUI.Session;
using System.Collections.ObjectModel;

namespace RecipeAppUI.ViewModels;

public class FavouritesViewModel : ViewModelBase 
{
    private MainWindowViewModel _mainWindowViewModel;
    private UserService _userService;
    private ObservableCollection<Recipe> _favourites;

    public UserService UserService
    {
        get => _userService;
        private set => _userService = value;
    }

    public MainWindowViewModel MainWindowViewModel
    {
        get => _mainWindowViewModel;
        private set => _mainWindowViewModel = value;    
    }

    public ObservableCollection<Recipe> Favourites 
    {
        get => _favourites;
        set => this.RaiseAndSetIfChanged(ref _favourites, value);
    }

    public FavouritesViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
    {
        UserService = new UserService(context, new PasswordEncrypter());
        MainWindowViewModel = mainWindowViewModel;
        Favourites = new ObservableCollection<Recipe>(new SearchByUserFavorite(context, UserSingleton.GetInstance()).FilterRecipes());
    }
}