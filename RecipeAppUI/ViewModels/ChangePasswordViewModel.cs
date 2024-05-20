namespace RecipeAppUI.ViewModels;

using ReactiveUI;
using System;
using RecipeApp.Services;
using RecipeApp.Context;
using System.Reactive;
using RecipeApp.Exceptions;

public class ChangePasswordViewModel : ViewModelBase {
    private string _username = null!;
    private string _oldPassword = null!;
    private string _newPassword = null!;
    private string _errorMessage = null!;
    private UserService _userService = null!;
    private MainWindowViewModel _mainWindowViewModel = null!;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string OldPassword { get => _oldPassword; set => this.RaiseAndSetIfChanged(ref _oldPassword, value); }
    public string NewPassword { get => _newPassword; set => this.RaiseAndSetIfChanged(ref _newPassword, value); }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public ReactiveCommand<Unit, Unit> ChangePasswordCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database connection</param>
    /// <param name="mainWindowViewModel">Used for navigation</param>
    public ChangePasswordViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        UserService = new(context, new());
        ChangePasswordCommand = ReactiveCommand.Create(ChangePassword);
        MainWindowViewModel = mainWindowViewModel;
    }

    /// <summary>
    /// Changes a users password
    /// </summary>
    private void ChangePassword() {
        try {
            var user = UserService.Login(Username, OldPassword);
            UserService.ChangePassword(user, NewPassword);
            MainWindowViewModel.ChangeToLoginView();
        } catch (ArgumentException e) {
            ErrorMessage = e.Message;
        } catch (UserDoesNotExistException e) {
            ErrorMessage = e.Message;
        } catch (InvalidCredentialsException e) {
            ErrorMessage = e.Message;
        }
    }
}