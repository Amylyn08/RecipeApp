using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Context;
using ReactiveUI;

namespace RecipeAppUI.ViewModels;

public class ProfileViewModel : ViewModelBase {
    private MainWindowViewModel _mainWindowViewModel = null!;    
    private string _username = null!;
    private string _description = null!;
    private byte[]? _profilePicture;
    private UserService _userService = null!;

    public MainWindowViewModel MainWindowViewModel {
        get => _mainWindowViewModel;
        set => _mainWindowViewModel = value;
    }

    public string Username {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Description {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public byte[] ProfilePicture {
        get => _profilePicture;
        set => this.RaiseAndSetIfChanged(ref _profilePicture, value);
    }

    public UserService {
        get => _userService;
        set => _userService = value;
    }

    public ProfileViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        MainWindowViewModel = mainWindowViewModel;
        UserService = new(context, new PasswordEncrypter());
        Username = UserSingleton.GetInstance().Username;
        Description = UserSingleton.GetInstance().Description;
        ProfilePicture = UserSingleton.GetInstance().ProfilePicture;
    }
}