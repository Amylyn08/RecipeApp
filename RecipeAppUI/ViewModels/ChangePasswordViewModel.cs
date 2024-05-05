namespace RecipeAppUI.ViewModels;

using ReactiveUI;
using System;
using RecipeApp.Services;
using RecipeApp.Context;
using RecipeApp.Models;
using System.Reactive;
using RecipeApp.Exceptions;
using RecipeAppUI.ViewModels;

public class ChangePasswordViewModel : ViewModelBase {
    private string _username;
    private string _oldPassword;
    private string _newPassword;
    private string _errorMessage;
    private UserService _userService;
    private MainWindowViewModel _mainWindowViewModel;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string OldPassword { get => _oldPassword; set => this.RaiseAndSetIfChanged(ref _oldPassword, value); }
    public string NewPassword { get => _newPassword; set => this.RaiseAndSetIfChanged(ref _newPassword, value); }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
    public UserService UserService { get => _userService; private set => _userService = value; }
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public ReactiveCommand<Unit, Unit> ChangePasswordCommand { get; }

    public ChangePasswordViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        UserService = new(context, new());
        ChangePasswordCommand = ReactiveCommand.Create(ChangePassword);
        MainWindowViewModel = mainWindowViewModel;
    }

    public void ChangePassword() {}

}