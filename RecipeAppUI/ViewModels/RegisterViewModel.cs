using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Services;
using System;
using System.Reactive;

namespace RecipeAppUI.ViewModels;

/// <summary>
/// ViewModel for the register page
/// </summary>
public class RegisterViewModel : ViewModelBase {
    private string _username = null!;
    private string _password = null!;
    private string _description = null!;
    private string _registerErrorMessage = null!;
    private UserService _userService = null!;
    private readonly MainWindowViewModel _mainWindowViewModel = null!;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
    public string RegisterErrorMessage { get => _registerErrorMessage; set => this.RaiseAndSetIfChanged(ref _registerErrorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

    /// <summary>
    /// Constructor for RegisterViewModel
    /// </summary>
    /// <param name="context">To pass it through the user service</param>
    /// <param name="mainWindowViewModel">For navigation</param>
    public RegisterViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        UserService = new(context, new());
        RegisterCommand = ReactiveCommand.Create(Register);
        _mainWindowViewModel = mainWindowViewModel;
    }

    /// <summary>
    /// Register a new user and redirected to login
    /// </summary>
    private void Register() {
        try {
            UserService.Register(Username, Password, Description);
            _mainWindowViewModel.ChangeToLoginView();
        } catch (UserAlreadyExistsException e) {
            RegisterErrorMessage = e.Message;
        } catch (ArgumentException e) {
            RegisterErrorMessage = e.Message;
        }
    }
}