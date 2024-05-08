using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeApp.Security;
using System.Collections.Generic;
using ReactiveUI;
using RecipeAppUI.User;

namespace RecipeAppUI.ViewModels;

public class FavouritesViewModel : ViewModelBase 
{
    private MainWindowViewModel _mainWindowViewModel;
    private UserService _userService;
    private List<Recipe> _favourites = UserSingleton.instance.Favorites;

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

    public List<Recipe> Favourites 
    {
        get => _favourites;
        set => this.RaiseAndSetIfChanged(ref _favourites, value);
    }

    public FavouritesViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel)
    {
        UserService = new UserService(context, new PasswordEncrypter());
        MainWindowViewModel = mainWindowViewModel;
    }
}