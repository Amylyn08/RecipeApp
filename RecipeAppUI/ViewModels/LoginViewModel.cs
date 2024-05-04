using ReactiveUI;
using RecipeApp.Services;
using RecipeApp.Context;
using System.Reactive;
using System;
using RecipeApp.Exceptions;

namespace RecipeAppUI.ViewModels;

public class LoginViewModel : ViewModelBase {
    private string _username;
    private string _password;
    private string _loginErrorMessage;
    private UserService _userService;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string LoginErrorMessage { get => _loginErrorMessage; set => this.RaiseAndSetIfChanged(ref _loginErrorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public LoginViewModel(SplankContext context) {
        UserService = new(context, new());
        LoginCommand = ReactiveCommand.Create(Login);
    }

    public void Login() {
        try {
            var user = UserService.Login(Username, Password);
        } catch (InvalidCredentialsException e) {
            LoginErrorMessage = e.Message;
        } catch (UserDoesNotExistException e) {
            LoginErrorMessage = e.Message;
        } catch (ArgumentException e) {
            LoginErrorMessage = e.Message;
        }
    }
}