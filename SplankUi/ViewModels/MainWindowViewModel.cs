using RecipeApp.Services;
using RecipeApp.Security;
using RecipeApp.Context;
using ReactiveUI;
namespace SplankUi.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    public string Greeting => "FUCK YOU!";
    public MainWindowViewModel() {
        LOR = new LoginViewAndRegisterViewModel();
        _contentViewModel = LOR;
    }


    public LoginViewAndRegisterViewModel LOR {get;}

    public ViewModelBase ContentViewModel {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void Authentication() {
        ContentViewModel = new LoginViewAndRegisterViewModel();
    }
}
