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
using System.Reactive;

namespace RecipeAppUI.ViewModels;

public class ProfileViewModel : ViewModelBase {
    private MainWindowViewModel _mainWindowViewModel = null!;    
    private string _username = null!;
    private string _description = null!;
    private byte[]? _profilePicture;
    private UserService _userService = null!;
    private string? _errorMessage; 

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

    public UserService UserService {
        get => _userService;
        set => _userService = value;
    }

    public string ErrorMessage {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public ReactiveCommand<Unit, Unit> ChooseImageCommand { get; }

    public ProfileViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) {
        MainWindowViewModel = mainWindowViewModel;
        UserService = new(context, new PasswordEncrypter());
        Username = UserSingleton.GetInstance().Name;
        Description = UserSingleton.GetInstance().Description;
        ProfilePicture = UserSingleton.GetInstance().ProfilePicture;
        ChooseImageCommand = ReactiveCommand.CreateFromTask(ChooseImage);
    }

   private async Task ChooseImage(){
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "jpg", "jpeg", "png", "gif", "bmp" } });

        string[] result = await dialog.ShowAsync(new());
        if (result.Length > 0)
        {
            string imagePath = result[0];
            using (FileStream stream = File.OpenRead(imagePath))
            {
                ProfilePicture = await stream.ReadAllBytesAsync();
            }
        }
    }
}