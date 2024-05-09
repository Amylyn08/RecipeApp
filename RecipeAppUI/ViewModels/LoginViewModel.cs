using ReactiveUI;
using RecipeApp.Services;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;
using System.Reactive;
using System;
using RecipeApp.Exceptions;
using RecipeAppUI.Session;
using System.Collections.Generic;


namespace RecipeAppUI.ViewModels;

public class LoginViewModel : ViewModelBase {
    private string _username;
    private string _password;
    private string _loginErrorMessage ="";
    private UserService _userService = null;
    private MainWindowViewModel _mainWindowViewModel;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string LoginErrorMessage { get => _loginErrorMessage; set => this.RaiseAndSetIfChanged(ref _loginErrorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public LoginViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        UserService = new(context, new());
        LoginCommand = ReactiveCommand.Create(Login);
        _mainWindowViewModel = mainWindowViewModel;
    }

    public void Login() {
        try {
            RecipeApp.Models.User user = UserService.Login(Username, Password);
            List<Recipe> favouriteRecipes = new SearchByUserFavorite(SplankContext.GetInstance(), user).FilterRecipes();
            user.Favorites = favouriteRecipes;
            UserSingleton.InstantiateUserOnce(user); // we now have a global user
            _mainWindowViewModel.ChangeToDashboardView();
        } catch (InvalidCredentialsException e) {
            LoginErrorMessage = e.Message;
        } catch (UserDoesNotExistException e) {
            LoginErrorMessage = e.Message;
        } catch (ArgumentException e) {
            LoginErrorMessage = e.Message;
        }
    }
}