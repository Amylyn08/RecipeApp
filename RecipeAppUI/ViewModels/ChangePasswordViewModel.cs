namespace Recipeui.ViewModel;

using ReactiveUI;
using RecipeApp.Services;
using RecipeApp.Context;
using RecipeApp.Models;
using System.Reactive;
using System;
using RecipeApp.Exceptions;

public class ChangePasswordViewModel : ViewModelBase {
    private string _username;
    private string _oldpassword;
    private string _newpassword;
    private string _errorMessage;
    private UserService _userService = null;
    private MainWindowViewModel _mainWindowViewModel;

    public string Username { get => _username; set => this.RaiseAndSetIfChanged(ref _username, value); }

    public ChangePasswordViewModel(SplankContext context, UserService userService, MainWindowViewModel mainWindowViewModel) {
    }
}