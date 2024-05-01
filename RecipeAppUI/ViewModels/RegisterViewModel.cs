using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Services;

namespace RecipeAppUI.ViewModels;

public class RegisterViewModel : ViewModelBase {
    private string _username;
    private string _password;
    private string _registerErrorMessage;
    private UserService _userService;

    public string Username { get => _username; private set => this.RaiseAndSetIfChanged(ref _username, value); }
    public string Password { get => _password; private set => this.RaiseAndSetIfChanged(ref _password, value); }
    public string RegisterErrorMessage { get => _registerErrorMessage; private set => _registerErrorMessage = value; }
    public UserService UserService { get => _userService; private set => _userService = value; }

    public RegisterViewModel(SplankContext context) {
        UserService = new(context, new());
    }
}