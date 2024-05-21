using Avalonia.Media.Imaging;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using ReactiveUI;

namespace RecipeAppUI.ViewModels;

/// <summary>
/// ViewModel for viewing someone's elses profile
/// </summary>
public class OtherProfileViewModel : ViewModelBase {
   private MainWindowViewModel _mainWindowViewModel = null!;    
    private string _username = null!;
    private string _description = null!;
    private byte[]? _profilePicture;
    private UserService _userService = null!;
    private string _errorMessage = null!; 
    private Bitmap? _bitmap;
    private string? _pathToImage;

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

    public byte[]? ProfilePicture {
        get => _profilePicture;
        set => this.RaiseAndSetIfChanged(ref _profilePicture, value);
    }

    public UserService UserService {
        get => _userService;
        set => _userService = value;
    }

    public string ErrorMessage {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public string? PathToImage {
        get => _pathToImage;
        set => this.RaiseAndSetIfChanged(ref _pathToImage, value);
    }

    public Bitmap? Bitmap {
        get => _bitmap;
        set => this.RaiseAndSetIfChanged(ref _bitmap, value);
    }


    public OtherProfileViewModel(SplankContext context, User user) {
        UserService = new(context, new());
        OtherPersonFavourites = new SearchByUserFavorite(context, user).FilterRecipes();
    }
}