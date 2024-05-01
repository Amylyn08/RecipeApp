using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Services;
using System;

namespace RecipeAppUI.ViewModels;

public class RegisterViewModel : ViewModelBase {
    private string _username;
    private string _password;
    private string _description;
    private string _registerErrorMessage;
    private UserService _userService;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
    public string RegisterErrorMessage { get => _registerErrorMessage; set => this.RaiseAndSetIfChanged(ref _registerErrorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }

    public RegisterViewModel(SplankContext context) {
        UserService = new(context, new());
    }

    public void Register()
    {
        try
        {
            UserService.Register(Username, Password, Description);
        }
        catch (UserAlreadyExistsException e)
        {
            RegisterErrorMessage = e.Message;
        }
        catch (ArgumentException e)
        {
            RegisterErrorMessage = e.Message;
        }
    }
}