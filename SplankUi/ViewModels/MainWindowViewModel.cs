using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Context;
namespace SplankUi.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public MainWindowViewModel() {
        LOR = new LoginViewAndRegisterViewModel();
    }

    public LoginViewAndRegisterViewModel LOR {get;}
}
