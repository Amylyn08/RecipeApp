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

/// <summary>
/// ViewModel for the login page
/// </summary>
public class LoginViewModel : ViewModelBase {
    private string _username = null!;
    private string _password = null!;
    private string _loginErrorMessage ="";
    private UserService _userService = null!;
    private MainWindowViewModel _mainWindowViewModel = null!;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string LoginErrorMessage { get => _loginErrorMessage; set => this.RaiseAndSetIfChanged(ref _loginErrorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    /// <summary>
    /// Constructs a new LoginViewModel
    /// </summary>
    /// <param name="context">For passing it in the user service</param>
    /// <param name="mainWindowViewModel">For navigation within the model</param>
    public LoginViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        UserService = new(context, new());
        LoginCommand = ReactiveCommand.Create(Login);
        _mainWindowViewModel = mainWindowViewModel;
    }

    /// <summary>
    /// Calls the library method to login a user
    /// and redirects to the dashboard
    /// </summary>
    private void Login() {
        try {
            User user = UserService.Login(Username, Password);
            UserSingleton.InstantiateUserOnce(user); 
            _mainWindowViewModel.ChangeToDashboardView();
        } catch (InvalidCredentialsException e) {
            LoginErrorMessage = e.Message;
        } catch (UserDoesNotExistException e) {
            LoginErrorMessage = e.Message;
        } catch (ArgumentException e) {
            LoginErrorMessage = e.Message;
        } catch (Exception e) {
            LoginErrorMessage = e.Message;
        }
    }
}