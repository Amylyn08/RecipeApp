using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Context;
using RecipeApp.Models;
using ReactiveUI;
using RecipeAppUI.Session;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using System.IO;
using System;
using System.Reactive;

namespace RecipeAppUI.ViewModels;

public class ProfileViewModel : ViewModelBase {
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

    public ReactiveCommand<Unit, Unit> ChooseImageCommand { get; }

    public ProfileViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        MainWindowViewModel = mainWindowViewModel;
        UserService = new(context, new PasswordEncrypter());
        Username = UserSingleton.GetInstance().Name;
        Description = UserSingleton.GetInstance().Description;
        ProfilePicture = UserSingleton.GetInstance().ProfilePicture;
        ChooseImageCommand = ReactiveCommand.Create(ChooseImage);
        SetupBitmapOnViewLoad();
    }

   private void ChooseImage(){
       try {
            if (PathToImage == null) {
                return;
            }
            PathToImage = PathToImage.Trim('"'); // remove quotes from ctrl + c, ctrl + v
            byte[] bytes = File.ReadAllBytes(PathToImage);
            UserService.SetProfilePicture(bytes, UserSingleton.GetInstance());
            using MemoryStream stream = new(bytes);
            Bitmap = new Bitmap(stream);
        } catch (FileNotFoundException) {
            ErrorMessage = "File not found, please check the path again, or try copy pasting";
        } catch (ArgumentException e) {
            ErrorMessage = e.Message;
        } catch (Exception) {
            ErrorMessage = "The image is too large, or too high quality";
        }
    }

    private void SetupBitmapOnViewLoad() {
        if (ProfilePicture is not null) {
            using MemoryStream stream = new MemoryStream(ProfilePicture);
            Bitmap = new Bitmap(stream);
        }
    }
}